using WeatherApi.Domain.Entities;
using WeatherApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Domain.Interfaces;
using System.Threading.Tasks;
using WeatherApi.Infrastructure.Services;
using System.Linq;

namespace WeatherApi.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _dbContext;
        private readonly IWeatherApiService _weatherApiService;

        public WeatherRepository(WeatherDbContext dbContext, IWeatherApiService weatherApiService)
        {
            _dbContext = dbContext;
            _weatherApiService = weatherApiService;
        }

        public async Task<IEnumerable<Weather>> GetAllAsync()
        {
            return await _dbContext.Weathers.ToListAsync();
        }

        public async Task<Weather> GetByIdAsync(int id)
        {
            return await _dbContext.Weathers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Weather> CreateAsync(Weather weather)
        {
            if (weather == null)
                throw new ArgumentNullException(nameof(weather));

            _dbContext.Weathers.Add(weather);
            await _dbContext.SaveChangesAsync();
            return weather;
        }

        public async Task<int> UpdateAsync(int id, Weather weather)
        {
            if (weather == null) throw new ArgumentNullException(nameof(weather));

            var existingWeather = await _dbContext.Weathers.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWeather == null)
            {
                throw new Exception($"Weather data with ID {id} not found.");
            }

            existingWeather.City = weather.City;
            existingWeather.Country = weather.Country;
            existingWeather.Temperature = weather.Temperature;

            return await _dbContext.SaveChangesAsync(); // Save changes after update
        }

        public async Task<int> DeleteAsync(int id)
        {
            var weather = await _dbContext.Weathers.FirstOrDefaultAsync(w => w.Id == id);
            if (weather == null)
            {
                throw new Exception($"Weather data with ID {id} not found.");
            }

            _dbContext.Weathers.Remove(weather);
            return await _dbContext.SaveChangesAsync();
        }

        // Use the WeatherApiService to fetch weather data and save it
        public async Task<Weather> FetchWeatherAsync(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
                throw new ArgumentException("City name cannot be null or empty.", nameof(cityName));

            // Fetch the weather data from the external API
            var weatherData = await _weatherApiService.FetchWeatherAsync(cityName);

            if (weatherData == null)
            {
                throw new Exception("Failed to fetch weather data.");
            }

            // Save the fetched data to the database
            return await CreateAsync(weatherData);
        }
    }
}
