using WeatherApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApi.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<Weather>> GetAllAsync();
        Task<Weather> GetByIdAsync(int id);
        Task<Weather> CreateAsync(Weather weather);
        Task<int> UpdateAsync(int id, Weather weather);
        Task<int> DeleteAsync(int id);
        Task<Weather> FetchWeatherAsync(string cityName);
    }
}
