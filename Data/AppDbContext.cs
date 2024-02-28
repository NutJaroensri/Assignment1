using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group83.Models;
namespace GBC_Travel_Group83.Data
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<RentalCar> RentalCars { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify column types for properties
            modelBuilder.Entity<Hotel>()
                .Property(h => h.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Flight>()
                .Property(h => h.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity <RentalCar>()
                .Property(h => h.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Booking>()
                .Property(h => h.TotalPrice)
                .HasColumnType("decimal(18,2)");

        }
    }

}
