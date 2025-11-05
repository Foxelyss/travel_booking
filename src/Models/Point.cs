using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Point
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Region { get; set; }

    [Required]
    [MaxLength(128)]
    public required string City { get; set; }
}
