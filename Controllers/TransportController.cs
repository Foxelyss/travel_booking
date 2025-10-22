using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        [HttpPost("/")]
        public void AddTransporting(String name, DateTime departure, DateTime arrival, int departure_point, int arrival_point, int transporting_mean, int company, float price, int place_count)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpGet("/{id}")]
        public void AddTransporting(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpPatch("/{id}")]
        public void EditTransporting(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpDelete("/{id}")]
        public void RemoveTransporting(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }
    }
}
