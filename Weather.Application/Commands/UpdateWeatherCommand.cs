using MediatR;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application.Commands
{
    public record UpdateWeatherCommand(int Id, Weather Weather) : IRequest<int>;
}
