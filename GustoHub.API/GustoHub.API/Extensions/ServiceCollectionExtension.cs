namespace GustoHub.API.Extensions
{
    using GustoHub.Data;
    using GustoHub.Data.Common;
    using GustoHub.Services.Interfaces;
    using GustoHub.Services.Services;
    using Microsoft.EntityFrameworkCore;
 

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("GustoHubDbContextConnection")
               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<GustoHubDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            //services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
