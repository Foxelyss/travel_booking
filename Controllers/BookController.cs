using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public record Ticket(int id, String name, int transporting,
                               DateTime start, DateTime end, String startPoint,
                               String endPoint, float price,
                               String mean, String company,
                               String payment
          )
        {
        }

        [Authorize]
        [HttpGet("/bookings")]
        public List<Ticket> GetTicketsForUser(String email, long passport)
        {
            return null;
        }

        [Authorize]
        [HttpPost("book")]
        public String Book(int transporting, String name, String surname, String middle_name, String email, long passport, long phone)
        {

            return "Success";
        }

        [Authorize]
        [HttpPost("return")]
        public String ReturnTicket(long id)
        {

            return "Success";
        }
    }
}
