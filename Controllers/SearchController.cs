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
            return (from t in _context.Transportations
                    join p in _context.Points on t.DeparturePointId equals p.Id
                    join p2 in _context.Points on t.ArrivalPointId equals p2.Id
                    join tm in _context.TransportationMeans on t.Id equals tm.Transport
                    join tm2 in _context.TransportingMeans on tm.Mean equals tm2.Id
                    where t.DeparturePointId == point_a && t.ArrivalPointId == point_b && t.Arrival == DateTime.MinValue
                    select new { t, p, p2, tm, tm2 })
            .Skip(page * 10)
            .Take(10);
        }
    }
}
