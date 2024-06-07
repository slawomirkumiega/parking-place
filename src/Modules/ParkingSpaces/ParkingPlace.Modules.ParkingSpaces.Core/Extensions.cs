using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Modules.ParkingSpaces.Core.Services;
using ParkingPlace.Shared.Databases.Postgres;

namespace ParkingPlace.Modules.ParkingSpaces.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        {
            services.AddPostgres<ParkingSpaceDbContext>();
            services.AddUnitOfWork<ParkingSpaceDbContext>();

            services.AddTransient<IParkingSpaceService, ParkingSpaceService>();
            services.AddTransient<IParkingSpaceReservationService, ParkingSpaceReservationService>();

            return services;
        }
    }
}
