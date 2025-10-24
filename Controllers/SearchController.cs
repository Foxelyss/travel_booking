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
        public IEnumerable<Transportation> searchForTransport(int point_a, int point_b, int quantity, long wanted_time, int mean, int page)
        {
            return _context.Transportations.Where(p => p.DeparturePointId == point_a && p.ArrivalPointId == point_b && p.Arrival == DateTime.MinValue).Take(10);
        }
    }
}
