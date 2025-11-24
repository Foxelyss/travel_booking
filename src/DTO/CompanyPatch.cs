
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public class CompanyPatch
{
    public string Name { get; set; }
    public string RegistrationAddress { get; set; }
    public string Phone { get; set; }
    public string Inn { get; set; }
}