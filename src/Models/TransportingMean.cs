using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelBooking.Models;


public class TransportingMean
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; set; }
    [JsonIgnore]
    public IEnumerable<Transport>? Transportations { get; set; }
}
