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

        [HttpGet("all")]
        public IResult GetAllMeans()
        {
            var mean = _context.TransportMeans.Take(100);

            if (mean == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(mean);
        }

        public record TransportingMeanEdit(string name) { };

        [HttpPatch("{id}")]
        public IResult EditMean(int id, [FromBody] TransportingMeanEdit transportingMeanEdit)
        {
            var mean = _context.TransportMeans.FirstOrDefault(b => b.Id == id);

            if (mean == null) { return Results.NotFound(); }

            mean.Name = transportingMeanEdit.name;
            _context.SaveChanges();

            return Results.Ok();
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
