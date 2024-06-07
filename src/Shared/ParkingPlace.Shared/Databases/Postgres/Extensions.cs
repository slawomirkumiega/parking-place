using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingPlace.Shared.Repository;
using static ParkingPlace.Shared.Repository.IRepository;

namespace ParkingPlace.Shared.Databases.Postgres
{
    public static class Extensions
    {
        private const string ConnectionName = "postgres";
        internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            //
            // Tutaj można dodać migrację bazy danych
            // Na chwilę obecną aby to zrobić należy uruchomić:
            // dotnet ef database update --project Modules\Clients\ParkingPlace.Modules.Clients.Core/ParkingPlace.Modules.Clients.Core.csproj --startup-project Bootstrapper/ParkingPlace.Bootstrapper/ParkingPlace.Bootstrapper.csproj
            //

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(ConnectionName);
            services.AddDbContext<T>(x => x.UseNpgsql(connectionString));

            return services;
        }

        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
