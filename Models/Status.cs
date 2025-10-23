using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Status
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }
}
