using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record class TransportingRegistration(
        [Required] string Name,
        [Required] int? DeparturePoint,
        [Required] int? ArrivalPoint,
          [Required] DateTime? Departure,
        [Required] DateTime? Arrival,
        [Required] int[] TransportingMean,
        [Required] int? Company,
        [Required] decimal? Price,
        [Required] uint? PlaceCount,
         uint FreePlaceCount
               )
{

}
