using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelBooking.Models;

public class Account
{
    [Key]
    public Guid Id { get; set; }

    [Required, Phone, MaxLength(128)]
    public required string Phone { get; set; }

    [Required, MaxLength(128)]
    public required string Email { get; set; }

    [Required, MaxLength(255), JsonIgnore]
    public required string PasswordHash { get; set; }
}