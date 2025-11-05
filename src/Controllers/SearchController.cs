using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Data;
using TravelBooking.DTO;
using TravelBooking.Models;

namespace TravelBooking.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly StoreContext _context;

        public SearchController(StoreContext context)
        {
            _context = context;
        }

        public record TransportingResult(int id, String name, DateTime start, DateTime end,
                                         String startPoint, String endPoint,
                                         int arr, int dep, float price, String mean,
                                         String company, int places, int freePlaceQuantity)
        {
        }

        [HttpGet("search")]
        public IEnumerable<object> searchForTransport(int point_a, int point_b, long wanted_time, int mean, int page)
        {
            return _context.Transports
            .Include(point_a => point_a.DeparturePoint)
            .Include(point_b => point_b.ArrivalPoint)
            .Include(means => means.TransportingMeans)
            .Where(t => t.DeparturePointId == point_a && t.ArrivalPointId == point_b && t.TransportingMeans.Any(m => m.Id == mean))
            .OrderBy(x => MathF.Abs(x.Departure.ToFileTime() - wanted_time))
            .Skip(page * 10)
            .Take(10);
        }
    }
}
