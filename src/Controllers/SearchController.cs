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

        [HttpGet("search")]
        public IEnumerable<object> searchForTransport(int point_a, int point_b, DateTime wanted_time, int mean, int page)
        {
            return _context.Transports
            .Include(point_a => point_a.DeparturePoint)
            .Include(point_b => point_b.ArrivalPoint)
            .Include(means => means.TransportingMeans)
            .Where(t => t.DeparturePointId == point_a && t.ArrivalPointId == point_b && (t.TransportingMeans!.Any(m => m.Id == mean) || mean == -1) && t.Departure > DateTime.UtcNow)
            .OrderBy(x => x.Departure - wanted_time.ToUniversalTime())
            .Include(company => company.Company)
            .Select(o => new
            {
                Id = o.Id,
                Name = o.Name,
                Arrival = o.Arrival,
                ArrivalPoint = o.ArrivalPoint,
                // ArrivalPointId = o.ArrivalPointId,
                Departure = o.Departure,
                DeparturePoint = o.DeparturePoint,
                // DeparturePointId = o.DeparturePointId,
                Price = o.Price,
                TransportingMeans = o.TransportingMeans,
                // Company = null,
                CompanyId = o.CompanyId,
                PlaceCount = o.PlaceCount,
                FreePlaceCount = o.FreePlaceCount,
                CompanyName = o.Company!.Name,
            })
            .Skip(page * 10)
            .Take(10);
        }
    }
}
