using MediatR;
using WeatherApi.Application.Commands;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

public class FetchWeatherCommandHandler : IRequestHandler<FetchWeatherCommand, Weather>
{
    private readonly IWeatherApiService _weatherApiService;
    private readonly IWeatherRepository _weatherRepository;

    public FetchWeatherCommandHandler(IWeatherApiService weatherApiService, IWeatherRepository weatherRepository)
    {
        _weatherApiService = weatherApiService;
        _weatherRepository = weatherRepository;
    }

    public async Task<Weather> Handle(FetchWeatherCommand request, CancellationToken cancellationToken)
    {
        // Fetch weather data from external API
        var weatherData = await _weatherApiService.FetchWeatherAsync(request.CityName);

        if (weatherData != null)
        {
            // Ensure all the necessary fields are populated before saving
            weatherData.City = request.CityName; // Ensure City is set from the request parameter
            weatherData.Country = weatherData.Country ?? "Unknown"; // Ensure Country is set (if null)
            weatherData.Temperature = weatherData.Temperature > 0 ? weatherData.Temperature : 0; // Ensure valid Temperature value

            // Save the fetched data to the database
            return await _weatherRepository.CreateAsync(weatherData);
        }

        throw new Exception("Failed to fetch weather data.");
    }
}
