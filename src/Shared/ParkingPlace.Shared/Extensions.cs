using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using ParkingPlace.Shared.Databases.Postgres;

namespace ParkingPlace.Shared
{
    public static class Extensions
    {
        private const string ApiTitle = "ParkingPlace API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgres(configuration);
            services.AddControllers();
            services.AddSwaggerGen(swagger =>
            {
               
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiTitle,
                    Version = ApiVersion
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSharedLayer(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            return app;
        }
    }
}
