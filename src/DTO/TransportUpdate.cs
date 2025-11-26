namespace TravelBooking.DTO
{
    public class TransportUpdate
    {
        public string Name { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Arrival { get; set; }
        public int? DeparturePointId { get; set; }
        public int? ArrivalPointId { get; set; }
        public int? CompanyId { get; set; }
        public decimal? Price { get; set; }
        public uint? PlaceCount { get; set; }
    }
}