using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    public required int PassengerId { get; set; }

    [Required, MaxLength(128)]
    public required string Payment { get; set; }

    [Required]
    public required decimal Price { get; set; }

    public required int TransportationId { get; set; }

    public virtual Passenger? Passenger { get; set; }
    public virtual Transport? Transportation { get; set; }

    public BookStatus Status { get; set; } = BookStatus.Upcoming;

    public required Guid AccountId { get; set; }
    public virtual Account? Account { get; set; }

    [Required] public DateTime BookingDate { get; set; } = DateTime.Now;
}