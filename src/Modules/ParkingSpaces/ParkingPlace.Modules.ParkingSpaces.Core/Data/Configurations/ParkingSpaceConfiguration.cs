using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingPlace.Modules.ParkingSpaces.Core.Entities;

namespace ParkingPlace.Modules.ParkingSpaces.Core.Data.Configurations
{
    internal sealed class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.ParkingSpaceNumber).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>().IsRequired();
        }
    }
}
