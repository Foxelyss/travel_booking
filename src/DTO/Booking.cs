using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record class Booking(
 [Required] int? transporting,
    [Required, MaxLength(256)]
     string name,
    [Required, MaxLength(256)]
     string surname,
    [Required, MaxLength(12)]
     string passport,
         [MaxLength(256)]
     string? middleName
)
{

}
