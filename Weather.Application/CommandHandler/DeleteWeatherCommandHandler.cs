using MediatR;
using WeatherApi.Application.Commands;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Application.CommandHandlers
{
    public class DeleteWeatherCommandHandler : IRequestHandler<DeleteWeatherCommand, int>
    {
        private readonly IWeatherRepository _weatherRepository;

        public DeleteWeatherCommandHandler(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<int> Handle(DeleteWeatherCommand request, CancellationToken cancellationToken)
        {
            return await _weatherRepository.DeleteAsync(request.Id);
        }
    }
}
