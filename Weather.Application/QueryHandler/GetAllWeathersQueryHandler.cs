using MediatR;
using WeatherApi.Application.Queries;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Application.QueryHandlers
{
    public class GetAllWeathersQueryHandler : IRequestHandler<GetAllWeathersQuery, IEnumerable<Weather>>
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetAllWeathersQueryHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<IEnumerable<Weather>> Handle(GetAllWeathersQuery request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.GetAllAsync();
        }
    }
}
