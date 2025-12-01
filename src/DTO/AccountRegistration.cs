using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TravelBooking.DTO;

public record class AccountRegistration(
    [Required, Phone] string phone,
    [MaxLength(128), Required, EmailAddress] string email,
    [MaxLength(255), Required] string password,
    [MaxLength(256)] string? username)
{

}
