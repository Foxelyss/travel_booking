using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Data;
using TravelBooking.Models;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    [Route("api/transport")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly StoreContext _context;

        public TransportController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IResult AddTransporting([FromBody] TransportingRegistration registration)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            foreach (int a in registration.TransportingMean)
            {
                if (_context.TransportMeans.Find(a) == null)
                {
                    return Results.NotFound($"Transport mean id #{a} not found");
                }
            }

            if (_context.Points.Find(registration.ArrivalPoint.GetValueOrDefault()) == null)
            {
                return Results.NotFound($"ArrivalPoint id #{registration.ArrivalPoint.GetValueOrDefault()} not found");
            }

            if (_context.Points.Find(registration.DeparturePoint.GetValueOrDefault()) == null)
            {
                return Results.NotFound($"DeparturePoint id #{registration.DeparturePoint.GetValueOrDefault()} not found");
            }

            if (_context.Companies.Find(registration.Company.GetValueOrDefault()) == null)
            {
                return Results.NotFound($"Company id #{registration.Company.GetValueOrDefault()} not found");
            }
            var obj = _context.Transports.Add(new Transport
            {
                Name = registration.Name,
                Departure = registration.Departure.GetValueOrDefault().ToUniversalTime(),
                Arrival = registration.Arrival.GetValueOrDefault().ToUniversalTime(),
                DeparturePointId = registration.DeparturePoint.GetValueOrDefault(),
                ArrivalPointId = registration.ArrivalPoint.GetValueOrDefault(),
                CompanyId = registration.Company.GetValueOrDefault(),
                Price = registration.Price.GetValueOrDefault(),
                PlaceCount = registration.PlaceCount.GetValueOrDefault(),
                FreePlaceCount = registration.PlaceCount.GetValueOrDefault()
            });

            _context.SaveChanges();

            _context.TransportingMeans.AddRange(registration.TransportingMean.Distinct().ToArray().Select(id => new TransportingMeans { TransportationId = obj.Entity.Id, TransportingMeanId = id }));
            _context.SaveChanges();

            return Results.Ok();
        }

        [HttpGet("{id}")]
        public IResult GetTransporting(int id)
        {
            var transporting = _context.Transports.Find(id);

            if (transporting == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(transporting);
        }

        [HttpPatch("{id}")]
        public IResult EditTransporting(int id, [FromBody] TransportUpdateDto updateDto)
        {
            var transport = _context.Transports.Find(id);
            if (transport == null)
            {
                return Results.NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            if (updateDto.Name != null) transport.Name = updateDto.Name;
            if (updateDto.Departure.HasValue) transport.Departure = updateDto.Departure.Value;
            if (updateDto.Arrival.HasValue) transport.Arrival = updateDto.Arrival.Value;
            if (updateDto.DeparturePointId.HasValue) transport.DeparturePointId = updateDto.DeparturePointId.Value;
            if (updateDto.ArrivalPointId.HasValue) transport.ArrivalPointId = updateDto.ArrivalPointId.Value;
            if (updateDto.CompanyId.HasValue) transport.CompanyId = updateDto.CompanyId.Value;
            if (updateDto.Price.HasValue) transport.Price = updateDto.Price.Value;
            if (updateDto.PlaceCount.HasValue)
            {
                transport.PlaceCount = updateDto.PlaceCount.Value;
                transport.FreePlaceCount = updateDto.PlaceCount.Value;
            }

            _context.SaveChanges();
            return Results.Ok(transport);
        }


        [HttpDelete("{id}")]
        public IResult RemoveTransporting(int id)
        {
            Transport? point = _context.Transports.Find(id);

            if (point == null)
            {
                return Results.NotFound();
            }

            _context.Transports.Attach(point);
            _context.Transports.Remove(point);

            _context.SaveChanges();
            return Results.Ok();
        }
    }
}
