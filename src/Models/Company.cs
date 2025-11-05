using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Inn { get; set; }

    [Required]
    [MaxLength(128)]
    public required string RegistrationAddress { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Phone { get; set; }
}