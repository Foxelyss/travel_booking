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


        [HttpGet("name/{name}")]
        public Point searchForPoint(string name = "Томск")
        {
            return (Point)_context.Points.Where(p => EF.Functions.Like(p.Name, $"%{name}%")).Take(1);
        }

        [HttpGet("search/{name}")]
        public IEnumerable<Point> searchForPoints(string name)
        {
            return _context.Points.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
        }
        [HttpPost("")]
        public void AddPoint(string name, string region, string city)
        {
            _context.Points.Add(new Point { Name = name, Region = region, City = city });
            _context.SaveChanges();
        }

        [HttpGet("{id}")]
        public IResult GetPoint(int id)
        {
            var point = _context.Points.FirstOrDefault(b => b.Id == id);

            if (point == null) { return Results.NotFound(); }

            return Results.Ok(point);
        }

        [HttpPatch("{id}")]
        public void EditTransporting(int id)
        {

        }

        [HttpDelete("{id}")]
        public void RemoveTransporting(int id)
        {
            Point point = new Point() { Id = id, Name = "", Region = "", City = "" };

            _context.Points.Attach(point);
            _context.Points.Remove(point);

            _context.SaveChanges();
        }
    }
}
