using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;


public class TransportingMeans
{
    [Key]
    public int Id { get; set; }

    public int Mean { get; set; }

    public int Transport { get; set; }

    public virtual TransportingMean? TransportingMean { get; set; }
    public virtual Transportation? Transportation { get; set; }
}