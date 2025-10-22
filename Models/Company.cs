using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; }

    [Required]
    [MaxLength(128)]
    public string Inn { get; set; }

    [Required]
    [MaxLength(128)]
    public string RegistrationAddress { get; set; }

    [Required]
    [MaxLength(128)]
    public string Phone { get; set; }
}