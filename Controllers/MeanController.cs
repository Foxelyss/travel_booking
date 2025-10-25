using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelBooking.Controllers
{
    [Route("api/mean")]
    [ApiController]
    public class MeanController : ControllerBase
    {
        [HttpPost("")]
        public void AddMean(String name, DateTime departure, DateTime arrival, int departure_point, int arrival_point, int transporting_mean, int company, float price, int place_count)
        {

        }

        [HttpGet("{id}")]
        public void GetMean(int id)
        {

        }

        [HttpPatch("{id}")]
        public void EditMean(int id)
        {

        }

        [HttpDelete("{id}")]
        public void RemoveMean(int id)
        {

        }
    }
}
