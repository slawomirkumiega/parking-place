using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Modules.Clients.Core;

namespace ParkingPlace.Modules.Clients.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddClientsModule(this IServiceCollection services)
        {
            services.AddCoreLayer();
            return services;
        }

        public static IApplicationBuilder UseClientsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
