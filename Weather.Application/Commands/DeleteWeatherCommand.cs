using MediatR;

namespace WeatherApi.Application.Commands
{
    public record DeleteWeatherCommand(int Id) : IRequest<int>;
}
