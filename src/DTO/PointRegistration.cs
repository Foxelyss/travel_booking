using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record PointRegistration(
    [Required, MinLength(1), MaxLength(128)] string name,
    [Required, MinLength(1), MaxLength(128)] string region,
    [Required, MinLength(1), MaxLength(128)] string city)
{ };