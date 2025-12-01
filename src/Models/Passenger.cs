using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Passenger
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(12)]
    public required string Passport { get; set; }

    [Required, MaxLength(32)]
    public required string Firstname { get; set; }

    [Required, MaxLength(32)]
    public required string Surname { get; set; }

    [MaxLength(32)]
    public string? Middlename { get; set; }
}


