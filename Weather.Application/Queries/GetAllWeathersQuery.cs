using MediatR;
using WeatherApi.Domain.Entities;
using System.Collections.Generic;

namespace WeatherApi.Application.Queries
{
    public record GetAllWeathersQuery() : IRequest<IEnumerable<Weather>>;
}
