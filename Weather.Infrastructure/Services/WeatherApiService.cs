using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;
using WeatherApi.Infrastructure.Models;

namespace WeatherApi.Infrastructure.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherApiService> _logger;

        public WeatherApiService(HttpClient httpClient, ILogger<WeatherApiService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Weather> FetchWeatherAsync(string cityName)
        {
            string apiKey = "18fc9643295e444acc923d3aa2cb3e23";
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}";

            try
            {
                _logger.LogInformation("Fetching weather data for city: {CityName}", cityName);

                // Get the response as a string (raw response)
                var response = await _httpClient.GetStringAsync(url);

                // Log the raw response from the API
                _logger.LogDebug("Raw response from OpenWeatherMap API: {Response}", response);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Empty response from weather API.");
                }

                // Deserialize the response into WeatherApiResponse
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var weatherData = JsonSerializer.Deserialize<WeatherApiResponse>(response ,options);

                // Log parsed weather data to ensure it's being correctly deserialized
                //_logger.LogDebug("Parsed weather datas: {@WeatherData}", weatherData);
                _logger.LogDebug("Parsed weather data: City={City}, Country={Country}, Temperature={Temperature}",
                    weatherData?.Name,
                    weatherData?.Sys?.Country,
                    weatherData?.Main?.Temp);

                if (weatherData == null)
                {
                    throw new Exception("Failed to parse weather data.");
                }

                // Map to the Weather domain entity
                var weather = new Weather
                {
                    City = weatherData.Name,  // City name
                    Country = weatherData.Sys?.Country ?? "Unknown",  // Country name
                    Temperature = weatherData.Main?.Temp.HasValue == true ? weatherData.Main.Temp.Value - 273.15 : 0.0  // Temperature in Celsius
                };

                // Log the Weather entity
                _logger.LogDebug("Mapped Weather entity: {@Weather}", weather);

                return weather;  // Returning Weather entity
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error fetching weather data from the API.");
                throw new Exception("Error fetching weather data from the API.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error parsing weather data.");
                throw new Exception("Error parsing weather data.", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching weather data.");
                throw new Exception("An unexpected error occurred while fetching weather data.", ex);
            }
        }



    }
}
