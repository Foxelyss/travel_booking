namespace TravelBooking.DTO;

public record TransportingRegistration(
        int id,
        String name,
        DateTime departure,
        DateTime arrival,
        int departurePoint,
        int arrivalPoint,
        int transportingMean,
        int company,
        float price,
        int placeCount,
        int freePlaceCount)
{

}
