using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Data.Configurations
{
    internal sealed class ParkingSpaceReservationsConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.ParkingSpace).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
        }
    }
}
