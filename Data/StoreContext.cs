using System;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Models;

namespace TravelBooking.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<TransportingMean> TransportingMeans { get; set; }
    public DbSet<TransportingMeans> TransportationMeans { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<Transportation> Transportations { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransportingMeans>()
            .HasOne(tm => tm.TransportingMean)
            .WithMany()
            .HasForeignKey(tm => tm.Mean);

        modelBuilder.Entity<TransportingMeans>()
            .HasOne(tm => tm.Transportation)
            .WithMany()
            .HasForeignKey(tm => tm.Transport);

        modelBuilder.Entity<Transportation>()
            .HasOne(t => t.DeparturePoint)
            .WithMany()
            .HasForeignKey(t => t.DeparturePointId);

        modelBuilder.Entity<Transportation>()
            .HasOne(t => t.ArrivalPoint)
            .WithMany()
            .HasForeignKey(t => t.ArrivalPointId);

        modelBuilder.Entity<Transportation>()
            .HasOne(t => t.Company)
            .WithMany()
            .HasForeignKey(t => t.CompanyId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Passenger)
            .WithMany()
            .HasForeignKey(b => b.PassengerId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Transportation)
            .WithMany()
            .HasForeignKey(b => b.TransportationId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Status)
            .WithOne()
            .HasForeignKey<Book>(b => b.StatusId);

        base.OnModelCreating(modelBuilder);
    }
}
