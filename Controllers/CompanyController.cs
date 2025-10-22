using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelBooking.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost("/")]
        public void AddCompany(String name, DateTime departure, DateTime arrival, int departure_point, int arrival_point, int transporting_mean, int company, float price, int place_count)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpGet("/{id}")]
        public void AddCompany(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpPatch("/{id}")]
        public void EditCompany(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }

        [HttpDelete("/{id}")]
        public void RemoveCompany(int id)
        {
            transportService.createItem(new Transporting(0, name, departure,
                    arrival, departure_point, arrival_point, transporting_mean, company, price, place_count, place_count));
        }
    }
}
