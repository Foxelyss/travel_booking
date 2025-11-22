using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Data;
using TravelBooking.DTO;
using TravelBooking.Models;

namespace TravelBooking.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly StoreContext _context;
        private static readonly SemaphoreSlim _orderSemaphore = new SemaphoreSlim(1, 1);

        public BookController(StoreContext context)
        {
            _context = context;
        }
        public record Ticket(int id, String name, int transporting,
                               DateTime start, DateTime end, String startPoint,
                               String endPoint, float price,
                               String mean, String company,
                               String payment
          )
        {
        }

        [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
        [HttpGet("bookings")]
        public IEnumerable<Book> GetTicketsForUser()
        {
            Guid id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return _context.Books
                .Include(x => x.Account)
                .Include(x => x.Transportation)
                .Where(x => x.Account.Id == id);
        }

        [Authorize]
        [HttpPost("book")]
        public async Task<IResult> Book(int transporting, String name, String surname, String middle_name, String email, long passport, long phone)
        {
            Guid id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            await _orderSemaphore.WaitAsync();
            var transportingObj = _context.Transports.Find(transporting);

            if (transportingObj == null)
            {
                return Results.NotFound();
            }

            if (transportingObj.FreePlaceCount <= 0)
            {
                return Results.Conflict();
            }
            else
            {
                transportingObj.FreePlaceCount--;
                await _context.SaveChangesAsync();
            }

            _orderSemaphore.Release();

            var passenger = new Passenger
            {
                Firstname = name,
                Surname = surname,
                Lastname = middle_name,
                Passport = passport,
            };

            _context.Passengers.Add(passenger);

            var booking = new Book
            {
                AccountId = id,
                Payment = "MIR",
                Price = transportingObj.Price,
                PassengerId = passenger.Id
            };
            _context.Books.Add(booking);

            return Results.Ok();
        }

        [Authorize]
        [HttpPost("return")]
        public async Task<IResult> ReturnTicket(long id)
        {
            await _orderSemaphore.WaitAsync();

            var booking = _context.Books.Find(id);

            var transportingObj = _context.Transports.Find(id);

            if (transportingObj == null || booking == null)
            {
                return Results.NotFound();
            }

            if (transportingObj.FreePlaceCount <= transportingObj.PlaceCount)
            {
                transportingObj.FreePlaceCount++;
                await _context.SaveChangesAsync();
            }
            else
            {
                return Results.Conflict();
            }

            booking.Status = BookStatus.Cancelled;
            await _context.SaveChangesAsync();

            _orderSemaphore.Release();
            return Results.Ok();
        }
    }
}

