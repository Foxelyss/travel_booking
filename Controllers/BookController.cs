using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // Logger logger = LoggerFactory.Create(BookController.class);
        public record Ticket(int id, String name, int transporting,
                               DateTime start, DateTime end, String startPoint,
                               String endPoint, float price,
                               String mean, String company,
                               String payment
          )
        {
        }
        [HttpGet("/bookings")]
        public List<Ticket> GetTicketsForUser(String email, long passport)
        {
            return null;
        }

        [HttpPost("book")]
        public String Book(int transporting, String name, String surname, String middle_name, String email, long passport, long phone)
        {

            return "Success";
        }

        [HttpPost("return")]
        public String ReturnTicket(String email, long passport, long id)
        {

            return "Success";
        }

        // [HttpPost("echo")]
        // public Transporting ReturnTicket(Transporting transporting)
        // {
        //     return transporting;
        // }
        //     @ResponseStatus(HttpStatus.BAD_REQUEST)
        // @ExceptionHandler
        // public String handleBookingException(BookingException ex)
        //     {
        //         return ex.getMessage();
        //     }

        //     @ResponseStatus(HttpStatus.BAD_REQUEST)
        // @ExceptionHandler
        // public void handleException(Exception ex)
        //     {
        //         logger.error(ex.getMessage());
        //     }
        // }
    }
}
