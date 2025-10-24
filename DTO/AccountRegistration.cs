using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record AccountRegistration(
    [Required] long phone,
    [Required][MaxLength(128)] string email,
    [Required][MaxLength(255)] string password,
    [MaxLength(256)] string? username)
{

}
