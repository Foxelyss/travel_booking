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

namespace TravelBooking.Controllers;

[Route("api/booking")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly StoreContext _context;
    private static readonly SemaphoreSlim _orderSemaphore = new SemaphoreSlim(0, 1);

    public BookController(StoreContext context)
    {
        _context = context;
        _orderSemaphore.Release(1);
    }

    [HttpGet("bookings")]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public IEnumerable<Book> GetTicketsForUser()
    {
        Guid id = HttpContext.User.GetGuid();

        return _context.Books
            .Include(x => x.Account)
            .Include(x => x.Transportation)
            .Where(x => x.Account!.Id == id);
    }

    [HttpPost("book")]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IResult> Book([FromForm] Booking booking)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        Guid id = HttpContext.User.GetGuid();

        await _orderSemaphore.WaitAsync();
        var transportingObj = _context.Transports.Find(booking.transporting.GetValueOrDefault());
        Console.WriteLine(booking.transporting.GetValueOrDefault());

        if (transportingObj == null)
        {
            return Results.NotFound($"Transport #{booking.transporting} not found");
        }

        if (transportingObj.FreePlaceCount <= 0)
        {
            _orderSemaphore.Release();

            return Results.Conflict();
        }
        else
        {

            transportingObj.FreePlaceCount--;
            await _context.SaveChangesAsync();

            _orderSemaphore.Release();
        }


        var passenger = new Passenger
        {
            Firstname = booking.name,
            Surname = booking.surname,
            Middlename = booking.middleName,
            Passport = booking.passport,
        };

        _context.Passengers.Add(passenger);

        var booking_c = new Book
        {
            AccountId = id,
            Payment = "MIR",
            Price = transportingObj.Price,
            PassengerId = passenger.Id,
            TransportationId = booking.transporting.GetValueOrDefault()
        };
        _context.Books.Add(booking_c);

        return Results.Ok();
    }

    [Authorize]
    [HttpPost("return")]
    public async Task<IResult> ReturnTicket([FromForm] long id)
    {
        Guid userid = HttpContext.User.GetGuid();


        var booking = _context.Books.Find(id);
        await _orderSemaphore.WaitAsync();

        var transportingObj = _context.Transports.Find(id);

        if (transportingObj == null || booking == null)
        {
            _orderSemaphore.Release();

            return Results.NotFound();
        }

        if (booking.AccountId == userid)
        {
            _orderSemaphore.Release();

            return Results.Forbid();
        }

        if (transportingObj.FreePlaceCount <= transportingObj.PlaceCount)
        {

            transportingObj.FreePlaceCount++;
            await _context.SaveChangesAsync();

            _orderSemaphore.Release();
        }
        else
        {
            return Results.Conflict();
        }

        booking.Status = BookStatus.Cancelled;
        await _context.SaveChangesAsync();

        return Results.Ok();
    }
}


