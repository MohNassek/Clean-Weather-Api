using MediatR;
using WeatherApi.Application.Queries;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Application.QueryHandlers
{
    public class GetWeatherByIdQueryHandler : IRequestHandler<GetWeatherByIdQuery, Weather>
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetWeatherByIdQueryHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<Weather> Handle(GetWeatherByIdQuery request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.GetByIdAsync(request.Id);
        }
    }
}
