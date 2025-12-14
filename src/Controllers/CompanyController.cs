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
        [Produces<Company>]
        public IResult AddCompany(CompanyRegistration companyRegistration)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            var company = new Company
            {
                Name = companyRegistration.name,
                RegistrationAddress = companyRegistration.registrationAddress,
                Phone = companyRegistration.phone,
                Inn = companyRegistration.INN
            };
            _context.Companies.Add(company);

            _context.SaveChanges();

            return Results.Ok(company);
        }

        [HttpGet("{id}")]
        [Produces<Company>]
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
        [Produces<Company>]
        public IResult EditCompany(int id, [FromBody] CompanyPatch companyPatch)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            var company = _context.Companies.SingleOrDefault(c => c.Id == id);

            if (company == null)
            {
                return Results.NotFound();
            }


            if (companyPatch.Name != null)
            {
                company.Name = companyPatch.Name;
            }
            if (companyPatch.RegistrationAddress != null)
            {
                company.RegistrationAddress = companyPatch.RegistrationAddress;
            }
            if (companyPatch.Phone != null)
            {
                company.Phone = companyPatch.Phone;
            }
            if (companyPatch.Inn != null)
            {
                company.Inn = companyPatch.Inn;
            }

            _context.SaveChanges();

            return Results.Ok(company);
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
