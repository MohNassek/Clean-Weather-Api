using MediatR;
using WeatherApi.Application.Commands;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Application.CommandHandlers
{
    public class UpdateWeatherCommandHandler : IRequestHandler<UpdateWeatherCommand, int>
    {
        private readonly IWeatherRepository _weatherRepository;

        public UpdateWeatherCommandHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<int> Handle(UpdateWeatherCommand request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.UpdateAsync(request.Id, request.Weather);
        }
    }
}
