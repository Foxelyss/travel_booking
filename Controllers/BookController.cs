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
            string email = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            return _context.Books.Join(_context.Passengers, book => book.PassengerId, passenger => passenger.Id, (book, passenger) => new { book, passenger })
                .Include(x => x.book.Account)
                .Where(x => x.book.Account.Email == email)
                .Select(x => x.book);
        }

        [Authorize]
        [HttpPost("book")]
        public async Task<IResult> Book(int transporting, String name, String surname, String middle_name, String email, long passport, long phone)
        {
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


            return Results.Ok();
        }

        [Authorize]
        [HttpPost("return")]
        public async Task<IResult> ReturnTicket(long id)
        {
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

            booking.StatusId = 1;
            await _context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}

