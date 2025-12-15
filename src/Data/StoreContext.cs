using System;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Models;

namespace TravelBooking.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<TransportingMean> TransportMeans { get; set; }
    public DbSet<TransportingMeans> TransportingMeans { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<Transport> Transports { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Account>().HasIndex(u => u.Phone).IsUnique();

        modelBuilder.Entity<TransportingMeans>()
            .HasOne(tm => tm.TransportingMean)
            .WithMany()
            .HasForeignKey(tm => tm.TransportingMeanId);

        modelBuilder.Entity<TransportingMeans>()
            .HasOne(tm => tm.Transportation)
            .WithMany()
            .HasForeignKey(tm => tm.TransportationId);

        modelBuilder.Entity<Transport>()
            .HasOne(t => t.DeparturePoint)
            .WithMany()
            .HasForeignKey(t => t.DeparturePointId);

        modelBuilder.Entity<Transport>()
            .HasOne(t => t.ArrivalPoint)
            .WithMany()
            .HasForeignKey(t => t.ArrivalPointId);

        modelBuilder.Entity<Transport>()
               .HasMany(t => t.TransportingMeans)
               .WithMany(mean => mean.Transportations)
               .UsingEntity<TransportingMeans>();

        modelBuilder.Entity<Transport>()
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
            .HasOne(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AccountId);

        base.OnModelCreating(modelBuilder);
    }
}
