using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Modules.Clients.Core.Data;
using ParkingPlace.Modules.Clients.Core.Services;
using ParkingPlace.Modules.Clients.Shared;
using ParkingPlace.Shared.Databases.Postgres;

namespace ParkingPlace.Modules.Clients.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        {
            services.AddPostgres<ClientsDbContext>();

            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientsModuleApi, ClientsModuleApi>();

            return services;
        }
    }
}
