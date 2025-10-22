using Microsoft.AspNetCore.Mvc;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    public class AdministrationController
    {
        [HttpPost("/add_point")]
        public async Task<IActionResult> AddPoint(
                   [FromQuery] string name,
                   [FromQuery] string region,
                   [FromQuery] string town)
        {

            var point = new Point(-1, name, region, town);
            // await transportService.AddItemAsync(point);

            return (IActionResult)Results.Ok();
        }

        [HttpPost("/create_company")]
        public async Task<IActionResult> CreateCompany(
                [FromQuery] string name,
                [FromQuery] string address,
                [FromQuery] string inn,
                [FromQuery] string phone)
        {
            var company = new Company(-1, name, address, inn, phone);
            // await companyService.AddItemAsync(company);

            return (IActionResult)Results.Ok();
        }
    }
}