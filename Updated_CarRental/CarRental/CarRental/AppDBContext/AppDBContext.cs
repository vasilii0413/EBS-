using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRental.Data.BodyType;
using CarRental.Data.Fuel;
using CarRental.Data.Transmissions;
using CarRental.Data.Rentals;
using CarRental.Data.ReservationStatus;
using CarRental.Data.TypeOfDrivings;
using CarRental.Data.Vehicles;
using CarRental.Data.VehicleTypes;

namespace CarRental.Models.AppDBContext
{
    public class AppDBContext:IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<BodyType> BodyType { get; set; } = default!;

        public DbSet<Fuel> Fuel { get; set; } = default!;

        public DbSet<Rental> Rental { get; set; } = default!;

        public DbSet<ReservationStatus> ReservationStatus { get; set; } = default!;

        public DbSet<Transmission> Transmission { get; set; } = default!;

        public DbSet<TypeOfDriving> TypeOfDriving { get; set; } = default!;

        public DbSet<Vehicle> Vehicle { get; set; } = default!;

        public DbSet<VehicleType> VehicleType { get; set; } = default!;
    }
}
