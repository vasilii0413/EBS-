using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

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

        public DbSet<CarRental.Models.BodyType> BodyType { get; set; } = default!;

        public DbSet<CarRental.Models.Fuel> Fuel { get; set; } = default!;

        public DbSet<CarRental.Models.Rental> Rental { get; set; } = default!;

        public DbSet<CarRental.Models.ReservationStatus> ReservationStatus { get; set; } = default!;

        public DbSet<CarRental.Models.Transmission> Transmission { get; set; } = default!;

        public DbSet<CarRental.Models.TypeOfDriving> TypeOfDriving { get; set; } = default!;

        public DbSet<CarRental.Models.Vehicle> Vehicle { get; set; } = default!;

        public DbSet<CarRental.Models.VehicleType> VehicleType { get; set; } = default!;
    }
}
