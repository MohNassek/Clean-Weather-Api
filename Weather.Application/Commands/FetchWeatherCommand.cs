using MediatR;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application.Commands
{
    public record FetchWeatherCommand(string CityName) : IRequest<Weather>;
}
