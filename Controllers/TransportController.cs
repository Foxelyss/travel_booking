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

            var obj = _context.Transports.Add(new Transport
            {
                Name = registration.Name,
                Departure = registration.Departure.GetValueOrDefault(),
                Arrival = registration.Arrival.GetValueOrDefault(),
                DeparturePointId = registration.DeparturePoint.GetValueOrDefault(),
                ArrivalPointId = registration.ArrivalPoint.GetValueOrDefault(),
                CompanyId = registration.Company.GetValueOrDefault(),
                Price = registration.Price.GetValueOrDefault(),
                PlaceCount = registration.PlaceCount.GetValueOrDefault(),
                FreePlaceCount = registration.PlaceCount.GetValueOrDefault()
            });
            _context.SaveChanges();

            _context.TransportingMeans.AddRange(registration.TransportingMean.Select(id => new TransportingMeans { TransportationId = obj.Entity.Id, TransportingMeanId = id }));
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
        public void EditTransporting(int id)
        {

        }

        [HttpDelete("{id}")]
        public void RemoveTransporting(int id)
        {
            Transport point = new Transport() { Id = id, Name = "" };

            _context.Transports.Attach(point);
            _context.Transports.Remove(point);

            _context.SaveChanges();
        }
    }
}
