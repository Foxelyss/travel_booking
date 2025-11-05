using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Data;
using TravelBooking.Models;
using TravelBooking.DTO;

namespace TravelBooking.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly StoreContext _context;

        public CompanyController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("")]
        [Consumes("application/json")]
        public IResult AddCompany(CompanyRegistration companyRegistration)
        {
            _context.Companies.Add(new Company
            {
                Name = companyRegistration.name,
                RegistrationAddress = companyRegistration.address,
                Phone = companyRegistration.phone,
                Inn = companyRegistration.INN
            });

            _context.SaveChanges();

            return Results.Ok();
        }

        [HttpGet("{id}")]
        public IResult GetCompany(int id)
        {
            var company = _context.Companies.SingleOrDefault(c => c.Id == id);

            if (company == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(company);
        }

        [HttpPatch("{id}")]
        public IResult EditCompany(int id)
        {
            var company = _context.Companies.SingleOrDefault(c => c.Id == id);

            // company = new Company
            // {
            // }
            // ;

            _context.SaveChanges();

            return Results.Ok();
        }

        [HttpDelete("{id}")]
        public IResult RemoveCompany(int id)
        {
            var _company = _context.Companies.SingleOrDefault(c => c.Id == id);

            if (_company == null)
            {
                return Results.NotFound();
            }

            _context.Companies.Remove(_company);
            _context.SaveChanges();
            return Results.NoContent();
        }
    }
}
