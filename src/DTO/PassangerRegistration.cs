namespace TravelBooking.DTO;

public record class PassengerRegistration(long phone, String Email, String Surname, String name, String middleName, long passport)
{
}
