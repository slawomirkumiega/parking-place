using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Modules.ParkingSpaces.Core.Data;
using ParkingPlace.Modules.ParkingSpaces.Core.Repositories;
using ParkingPlace.Modules.ParkingSpaces.Core.Services;
using ParkingPlace.Shared.Databases.Postgres;

namespace ParkingPlace.Modules.ParkingSpaces.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        {
            services.AddPostgres<ParkingSpaceDbContext>();

            services.AddScoped<IParkingSpaceService, ParkingSpaceService>();
           /* services.AddScoped<IParkingSpaceReservationService, ParkingSpaceReservationService>();

            services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
            services.AddScoped<IParkingSpaceReservationRepository, ParkingSpaceReservationRepository>();*/

            return services;
        }
    }
}
