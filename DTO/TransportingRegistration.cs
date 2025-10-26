using System.ComponentModel.DataAnnotations;

namespace TravelBooking.DTO;

public record class TransportingRegistration(
        [Required] string Name,
        [Required] DateTime Departure,
        [Required] DateTime Arrival,
        [Required] int DeparturePoint,
        [Required] int ArrivalPoint,
        [Required] int TransportingMean,
        [Required] int Company,
        [Required] float Price,
        [Required] uint PlaceCount,
         uint FreePlaceCount)
{

}
