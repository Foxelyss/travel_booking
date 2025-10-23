using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record AccountRegistration(
    [Required] long Phone,
    [Required][MaxLength(128)] string Email,
    [Required][MaxLength(255)] string PasswordHash,
    [MaxLength(256)] string? Username)
{

}
