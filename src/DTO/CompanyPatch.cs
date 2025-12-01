
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public class CompanyPatch
{
    [MinLength(1), MaxLength(128)] public string Name { get; set; }
    [MinLength(1), MaxLength(128)] public string RegistrationAddress { get; set; }
    [MinLength(1), MaxLength(128)] public string Phone { get; set; }
    [MinLength(1), MaxLength(128)] public string Inn { get; set; }
}