using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Transport
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }

    [Required]
    public required DateTime Departure { get; set; }

    [Required]
    public required DateTime Arrival { get; set; }

    [Required]
    public required int DeparturePointId { get; set; }

    [Required]
    public required int ArrivalPointId { get; set; }

    [Required]
    public required int CompanyId { get; set; }

    [Required]
    public required decimal Price { get; set; }

    [Required]
    public required uint PlaceCount { get; set; }

    [Required]
    public uint FreePlaceCount { get; set; }

    public virtual Point? DeparturePoint { get; set; }
    public virtual Point? ArrivalPoint { get; set; }
    public virtual Company? Company { get; set; }

    public IEnumerable<TransportingMean>? TransportingMeans { get; set; }
}
