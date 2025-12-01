
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record class TransportingMeanPatch([MaxLength(128)] string name) { };