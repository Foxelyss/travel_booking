using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record CompanyRegistration(
    [Required, MaxLength(128)] string name,
    [Required, MaxLength(128)] string address,
    [Required, MaxLength(128)] string INN,
    [Required, MaxLength(128)] string phone)
{
}
