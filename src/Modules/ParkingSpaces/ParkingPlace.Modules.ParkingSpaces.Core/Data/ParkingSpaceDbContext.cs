using Microsoft.EntityFrameworkCore;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Data
{
    internal class ParkingSpaceDbContext(DbContextOptions<ParkingSpaceDbContext> options) : DbContext(options)
    {
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("parking-space");
        }
    }
}
