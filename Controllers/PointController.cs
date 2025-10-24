using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.DTO;
using TravelBooking.Models;

namespace TravelBooking.Controllers
{
    [Route("api/transport")]
    [ApiController]
    public class PointController : ControllerBase
    {


        [HttpGet("point")]
        public Point searchForPoint(String point = "Томск")
        {
            return null;
        }

        [HttpGet("points")]
        public List<Point> searchForPoints()
        {
            return null;
        }
        [HttpPost("/")]
        public void AddTransporting(String name, DateTime departure, DateTime arrival, int departure_point, int arrival_point, int transporting_mean, int company, float price, int place_count)
        {

        }

        [HttpGet("/{id}")]
        public void AddTransporting(int id)
        {

        }

        [HttpPatch("/{id}")]
        public void EditTransporting(int id)
        {

        }

        [HttpDelete("/{id}")]
        public void RemoveTransporting(int id)
        {

        }
    }
}
