namespace GustoHub.API.Extensions
{
    using GustoHub.Services.Interfaces;
    using GustoHub.Services.Services;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
