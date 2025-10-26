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

            var obj = _context.Transportations.Add(new Transportation
            {
                Name = registration.Name,
                Departure = registration.Departure,
                Arrival = registration.Arrival,
                DeparturePointId = registration.DeparturePoint,
                ArrivalPointId = registration.ArrivalPoint,
                CompanyId = registration.Company,
                Price = registration.Price,
                PlaceCount = registration.PlaceCount,
                FreePlaceCount = registration.PlaceCount
            });
            _context.SaveChanges();
            // _context.TransportationMeans.AddRange(registration.transportingMean.Select(id => new TransportingMeans { Transport = obj.Entity.Id, Mean = id }));
            _context.SaveChanges();

            return Results.Ok();
        }

        [HttpGet("{id}")]
        public IResult GetTransporting(int id)
        {
            var transporting = _context.Transportations.Find(id);

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
            Transportation point = new Transportation() { Id = id, Name = "" };

            _context.Transportations.Attach(point);
            _context.Transportations.Remove(point);

            _context.SaveChanges();
        }
    }
}
