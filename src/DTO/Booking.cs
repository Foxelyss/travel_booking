using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public class Booking
{
    [Required, MaxLength(256)]
    public string name;
    [Required, MaxLength(256)]
    public string surname;
    [Required, MaxLength(256)]
    public string middle_name;
    [Required, MaxLength(10)]
    public string passport;
}
