using Infrastructure.Ef.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class WaymateContext : DbContext
{
    public WaymateContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbTrip> Trip { get; set; }
    public DbSet<DbBooking> Booking { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Username).HasColumnName("username");
            entity.Property(u => u.Password).HasColumnName("password");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.IsBanned).HasColumnName("isBanned");
            entity.Property(u => u.BirthDate).HasColumnName("birthdate");
            entity.Property(u => u.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(u => u.LastName).HasColumnName("lastName");
            entity.Property(u => u.FirstName).HasColumnName("firstName");
            entity.Property(u => u.Gender).HasColumnName("gender");
            entity.Property(u => u.City).HasColumnName("city");
        });
        
        modelBuilder.Entity<DbTrip>(entity =>
        {
            entity.ToTable("trip");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.IdDriver).HasColumnName("idDriver");
            entity.Property(t => t.Smoke).HasColumnName("smoke");
            entity.Property(t => t.Price).HasColumnName("price");
            entity.Property(t => t.Luggage).HasColumnName("luggage");
            entity.Property(t => t.PetFriendly).HasColumnName("petFriendly");
            entity.Property(t => t.Date).HasColumnName("date");
            entity.Property(t => t.DriverMessage).HasColumnName("driverMessage");
            entity.Property(t => t.AirConditioning).HasColumnName("airConditioning");
            entity.Property(t => t.CityStartingPoint).HasColumnName("cityStartingPoint");
            entity.Property(t => t.CityDestination).HasColumnName("cityDestination");
            entity.Property(t => t.PlateNumber).HasColumnName("plateNumber");
            entity.Property(t => t.Brand).HasColumnName("brand");
            entity.Property(t => t.Model).HasColumnName("model");
        });
        
        modelBuilder.Entity<DbBooking>(entity =>
        {
            entity.ToTable("booking");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).HasColumnName("id");
            entity.Property(b => b.Date).HasColumnName("date");
            entity.Property(b => b.ReservedSeats).HasColumnName("reservedSeats");
            entity.Property(b => b.IdPassenger).HasColumnName("idPassenger");
            entity.Property(b => b.IdTrip).HasColumnName("idTrip");
        });
    }
}