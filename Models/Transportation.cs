using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Transportation
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; }

    [Required]
    public DateTime Departure { get; set; }

    [Required]
    public DateTime Arrival { get; set; }

    [Required]
    public int DeparturePointId { get; set; }

    [Required]
    public int ArrivalPointId { get; set; }

    public int CompanyId { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public uint PlaceCount { get; set; }

    [Required]
    public uint FreePlaceCount { get; set; }

    public virtual Point DeparturePoint { get; set; }
    public virtual Point ArrivalPoint { get; set; }
    public virtual Company Company { get; set; }
}
