using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Data;
using TravelBooking.Models;

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

        [HttpPost("/")]
        public IResult AddTransporting(String name, DateTime departure, DateTime arrival, int departure_point, int arrival_point, int transporting_mean, int company, float price, int place_count)
        {
            _context.Transportations.Add(new Transportation
            {
                Name = name,
            });

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
