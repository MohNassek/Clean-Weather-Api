using WeatherApi.Domain.Entities;

namespace WeatherApi.Domain.Interfaces
{
    public interface IWeatherApiService
    {
        Task<Weather> FetchWeatherAsync(string city);
    }
}
