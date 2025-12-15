using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record PointPatch(
    [MinLength(1), MaxLength(128)] string? name,
    [MinLength(1), MaxLength(128)] string? region,
    [MinLength(1), MaxLength(128)] string? city)
{ };