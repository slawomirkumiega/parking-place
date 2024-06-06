using Microsoft.EntityFrameworkCore;
using ParkingPlace.Modules.Clients.Core.Entities;

namespace ParkingPlace.Modules.Clients.Core.Data
{
    internal class ClientsDbContext(DbContextOptions<ClientsDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clients");
        }
    }
}
