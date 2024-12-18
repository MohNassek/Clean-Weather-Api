using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application.Commands
{
    public record AddWeatherCommand(Weather weather) : IRequest<Weather>;
    
}
