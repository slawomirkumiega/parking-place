using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Modules.ParkingSpaces.Core;

namespace ParkingPlace.Modules.ParkingSpaces.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddParkingSpacesModule(this IServiceCollection services)
        {
            services.AddCoreLayer();
            return services;
        }

        public static IApplicationBuilder UseParkingSpacesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
