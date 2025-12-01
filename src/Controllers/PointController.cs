using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Data;
using TravelBooking.DTO;
using TravelBooking.Models;

namespace TravelBooking.Controllers
{
    [Route("api/point")]
    [ApiController]
    public class PointController : ControllerBase
    {

        private readonly StoreContext _context;

        public PointController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public IResult GetPoints()
        {
            return Results.Ok(_context.Points.Take(2000));
        }



        [HttpPost("")]
        public IResult AddPoint([FromBody] PointRegistration pointAdd)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            var point = new Point { Name = pointAdd.name, Region = pointAdd.region, City = pointAdd.city };

            _context.Points.Add(point);
            _context.SaveChanges();

            return Results.Ok(point);
        }

        [HttpGet("{id}")]
        public IResult GetPoint(int id)
        {
            var point = _context.Points.FirstOrDefault(b => b.Id == id);

            if (point == null) { return Results.NotFound(); }

            return Results.Ok(point);
        }

        [HttpPatch("{id}")]
        public async Task<IResult> EditPoint(int id, [FromBody] PointPatch newPointData)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            var point = _context.Points.FirstOrDefault(b => b.Id == id);

            if (point == null) { return Results.NotFound(); }

            point.Name = newPointData.name;
            point.Region = newPointData.region;
            point.City = newPointData.city;

            await _context.SaveChangesAsync();

            return Results.Ok(point);
        }

        [HttpDelete("{id}")]
        public IResult RemovePoint(int id)
        {
            var point = _context.Points.Find(id);
            if (point == null) { return Results.NotFound(); }
            _context.Points.Remove(point);

            _context.SaveChanges();
            return Results.Ok();
        }
    }
}
