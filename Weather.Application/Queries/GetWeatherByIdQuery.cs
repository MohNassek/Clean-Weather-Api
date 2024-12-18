using MediatR;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application.Queries
{
    public record GetWeatherByIdQuery(int Id) : IRequest<Weather>;
}
