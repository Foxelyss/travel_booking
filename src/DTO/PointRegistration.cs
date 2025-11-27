using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record PointRegistration([Required, MinLength(2), MaxLength(255)] string name,
[Required, MinLength(2), MaxLength(255)] string region,
[Required, MinLength(2), MaxLength(255)] string city)
{ };