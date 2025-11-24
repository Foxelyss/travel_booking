using System.ComponentModel.DataAnnotations;
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

        [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
        [HttpGet("bookings")]
        public IEnumerable<Book> GetTicketsForUser()
        {
            Guid id = HttpContext.User.GetGuid();

            return _context.Books
                .Include(x => x.Account)
                .Include(x => x.Transportation)
                .Where(x => x.Account.Id == id);
        }

        [Authorize]
        [HttpPost("book/{transporting}")]
        public async Task<IResult> Book(Booking booking)
        {
            Guid id = HttpContext.User.GetGuid();

            await _orderSemaphore.WaitAsync();
            var transportingObj = _context.Transports.Find(booking.transporting);

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
                Firstname = booking.Name,
                Surname = booking.Surname,
                Middlename = booking.MiddleName,
                Passport = booking.passport,
            };

            _context.Passengers.Add(passenger);

            var booking_c = new Book
            {
                AccountId = id,
                Payment = "MIR",
                Price = transportingObj.Price,
                PassengerId = passenger.Id,
                TransportationId = booking.transporting
            };
            _context.Books.Add(booking_c);

            return Results.Ok();
        }

        [Authorize]
        [HttpPost("return")]
        public async Task<IResult> ReturnTicket([FromForm] long id)
        {
            Guid userid = HttpContext.User.GetGuid();

            await _orderSemaphore.WaitAsync();

            var booking = _context.Books.Find(id);

            var transportingObj = _context.Transports.Find(id);

            if (transportingObj == null || booking == null)
            {
                return Results.NotFound();
            }

            if (booking.AccountId == userid)
            {
                return Results.Forbid();
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

