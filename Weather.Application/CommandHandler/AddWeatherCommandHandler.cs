using MediatR;
using WeatherApi.Application.Commands;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

public class AddWeatherCommandHandler : IRequestHandler<AddWeatherCommand, Weather>
{
    private readonly IWeatherRepository _weatherRepository;

    public AddWeatherCommandHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<Weather> Handle(AddWeatherCommand request, CancellationToken cancellationToken)
    {
        // Ensure the City field is populated before saving
        if (string.IsNullOrEmpty(request.weather.City))
        {
            throw new ArgumentException("City field cannot be null or empty.");
        }

        // Save the weather data to the database
        return await _weatherRepository.CreateAsync(request.weather);
    }
}
