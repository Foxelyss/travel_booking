using System;
using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Models;


public class TransportingMeans
{
    [Key]
    public int Id { get; set; }

    public int TransportingMeanId { get; set; }

    public int TransportationId { get; set; }

    public virtual TransportingMean? TransportingMean { get; set; }
    public virtual Transport? Transportation { get; set; }
}