using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record CompanyRegistration(
    [Required, MaxLength(256)] string name, [Required, MaxLength(256)] string address,
    [Required, MaxLength(256)] string INN, [Required, MaxLength(256)] string phone)
{
}
