using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Required]
    public long Phone { get; set; }

    [Required]
    [MaxLength(128)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }

    [MaxLength(256)]
    public string Username { get; set; }
}