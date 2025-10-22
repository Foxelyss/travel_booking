using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    public int PassengerId { get; set; }

    [Required]
    [MaxLength(128)]
    public string Payment { get; set; }

    [Required]
    public float Price { get; set; }

    public int TransportationId { get; set; }

    public virtual Passenger Passenger { get; set; }
    public virtual Transportation Transportation { get; set; }

    public int StatusId;
    public virtual Status Status { get; set; }
}