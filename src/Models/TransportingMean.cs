using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelBooking.Models;


public class TransportingMean
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(128)]
    public required string Name { get; set; }
    [JsonIgnore]
    public ICollection<Transport>? Transportations { get; set; }
}
