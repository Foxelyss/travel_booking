using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record MeanRegistration(int id, [Required, MaxLength(255)] String name)
{
}
