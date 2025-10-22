using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Point
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; }

    [Required]
    [MaxLength(128)]
    public string Region { get; set; }

    [Required]
    [MaxLength(128)]
    public string City { get; set; }
}
