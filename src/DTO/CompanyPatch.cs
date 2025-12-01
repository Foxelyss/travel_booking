
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record class CompanyPatch(
    [MinLength(1), MaxLength(128)] string? Name,
    [MinLength(1), MaxLength(128)] string? RegistrationAddress,
    [MinLength(1), MaxLength(128)] string? Phone,
    [MinLength(1), MaxLength(128)] string? Inn
    )
{ }