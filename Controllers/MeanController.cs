using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Data;
using TravelBooking.Models;

namespace TravelBooking.Controllers
{
    [Route("api/mean")]
    [ApiController]
    public class MeanController : ControllerBase
    {

        private readonly StoreContext _context;

        public MeanController(StoreContext context)
        {
            _context = context;
        }
        [HttpPost("")]
        public void AddMean(string name)
        {
            _context.TransportMeans.Add(new TransportingMean { Name = name });
            _context.SaveChanges();
        }

        [HttpGet("{id}")]
        public IResult GetMean(int id)
        {
            var mean = _context.TransportMeans.FirstOrDefault(b => b.Id == id);
            if (mean == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(mean);
        }

        [HttpPatch("{id}")]
        public void EditMean(int id)
        {

        }

        [HttpDelete("{id}")]
        public IResult RemoveMean(int id)
        {
            var mean = _context.TransportMeans.FirstOrDefault(b => b.Id == id);

            if (mean == null)
            {
                return Results.NotFound();
            }

            _context.TransportMeans.Remove(mean);
            _context.SaveChanges();

            return Results.NoContent();
        }
    }
}
