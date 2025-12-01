using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Data;
using TravelBooking.DTO;
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
        public IResult AddMean([FromBody] TransportingMean mean)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            var transportingMean = new TransportingMean { Name = mean.Name };
            _context.TransportMeans.Add(transportingMean);
            _context.SaveChanges();

            return Results.Ok(transportingMean);
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


        [HttpPatch("{id}")]
        public IResult EditMean(int id, [FromBody] TransportingMeanPatch transportingMeanEdit)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

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
