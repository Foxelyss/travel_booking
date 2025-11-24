using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public class Booking
{
    [Required] public required int transporting;
    [Required, MaxLength(256)]
    public required string Name;
    [Required, MaxLength(256)]
    public required string Surname;
    [Required, MaxLength(256)]
    public required string MiddleName;
    [Required, MaxLength(10)]
    public required string passport;
}
