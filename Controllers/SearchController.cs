using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        public record TransportingResult(int id, String name, DateTime start, DateTime end,
                                         String startPoint, String endPoint,
                                         int arr, int dep, float price, String mean,
                                         String company, int places, int freePlaceQuantity)
        {
        }


        [HttpGet("point")]
        public Point searchForPoint(String point = "Томск")
        {
            return pointService.findByName(point);
        }

        [HttpGet("points")]
        public List<Point> searchForPoints()
        {
            return pointService.getAllItems();
        }

        [HttpGet("means")]
        public List<Mean> searchForMeans()
        {
            return transportService.getAllTransportingMeans();
        }

        [HttpGet("search")]
        public List<TransportingResult> searchForTransport(int point_a, int point_b, int quantity, long wanted_time, int mean, int page)
        {
            return transportService.findByDest(point_a, point_b, quantity, wanted_time, mean, page);
        }
    }
}
