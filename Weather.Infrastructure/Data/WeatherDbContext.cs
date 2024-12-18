using Microsoft.EntityFrameworkCore;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Infrastructure.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Weather> Weathers { get; set; }

    }
}
