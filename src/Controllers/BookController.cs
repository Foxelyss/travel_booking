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
    private static readonly SemaphoreSlim _orderSemaphore = new SemaphoreSlim(1, 1);

    public BookController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("bookings")]
    [Authorize()]
    public IEnumerable<Object> GetTicketsForUser()
    {
        Guid id = HttpContext.User.GetGuid();

        return _context.Books
            .Include(x => x.Account)
            .Where(x => x.Account!.Id == id)
            .Include(x => x.Transportation)
            .Include(point_a => point_a.Transportation!.DeparturePoint)
            .Include(point_b => point_b.Transportation!.ArrivalPoint)
            .Include(means => means.Transportation!.TransportingMeans)
            .Select(o => new
            {
                Id = o.Id,
                Transportation = new
                {
                    Id = o.Transportation!.Id,
                    Name = o.Transportation!.Name,
                    Arrival = o.Transportation!.Arrival,
                    ArrivalPoint = o.Transportation!.ArrivalPoint,
                    Departure = o.Transportation!.Departure,
                    DeparturePoint = o.Transportation!.DeparturePoint,
                    TransportingMeans = o.Transportation!.TransportingMeans,
                    PlaceCount = o.Transportation!.PlaceCount,
                    FreePlaceCount = o.Transportation!.FreePlaceCount,
                    CompanyName = o.Transportation!.Company!.Name,
                },
                BookingDate = o.BookingDate,
                Price = o.Price,
                Payment = o.Payment,
                StatusId = (int)o.Status,
                Status = o.Status.ToString()
            });
    }

    [HttpPost("book")]
    [Authorize()]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IResult> Book([FromForm] Booking booking)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (!booking.passport.Replace(" ", string.Empty).All(c => c >= '0' && c <= '9'))
        {
            return Results.BadRequest("Passport invalid");
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
        await _context.SaveChangesAsync();

        var booking_c = new Book
        {
            AccountId = id,
            Payment = "MIR",
            Price = transportingObj.Price,
            PassengerId = passenger.Id,
            TransportationId = booking.transporting.GetValueOrDefault()
        };

        _context.Books.Add(booking_c);
        await _context.SaveChangesAsync();

        return Results.Ok();
    }
    public record class Return([Required] long? id);

    [HttpPost("return")]
    [Authorize()]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IResult> ReturnTicket([FromForm] int id)
    {
        Guid userid = HttpContext.User.GetGuid();

        var booking = _context.Books.Find(id);

        await _orderSemaphore.WaitAsync();

        if (booking == null)
        {
            _orderSemaphore.Release();

            return Results.NotFound($"Booking #{id} not found");
        }

        if (booking.Status == BookStatus.Cancelled)
        {
            _orderSemaphore.Release();

            return Results.NotFound($"Can't cancel the cancelled!");
        }

        var transportingObj = _context.Transports.Find(booking.TransportationId);

        if (transportingObj == null)
        {
            _orderSemaphore.Release();

            return Results.NotFound($"Transport #{booking.TransportationId}  not found");
        }

        if (booking.AccountId != userid)
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


