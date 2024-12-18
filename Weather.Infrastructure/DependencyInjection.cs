using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherApi.Domain.Interfaces;
using WeatherApi.Infrastructure.Data;
using WeatherApi.Infrastructure.Repositories;
using WeatherApi.Infrastructure.Services;

namespace WeatherApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, string connectionString)
        {
            // Configure DbContext
            services.AddDbContext<WeatherDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Register Repositories and Services
            services.AddScoped<IWeatherRepository, WeatherRepository>();

            // Register HttpClient Factory for WeatherApiService
            services.AddHttpClient<IWeatherApiService, WeatherApiService>();

            return services;
        }
    }
}
