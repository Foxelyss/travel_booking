using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TravelBooking.Models;

public class Account
{
    [Key]
    public Guid Id { get; set; }

    [Required, Phone, MaxLength(128)]
    public required string Phone { get; set; }

    [Required, MaxLength(128)]
    public required string Email { get; set; }

    [Required, MaxLength(255)]
    public required string PasswordHash { get; set; }

    [MaxLength(255)]
    public string? Username { get; set; }
}