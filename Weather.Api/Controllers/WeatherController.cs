using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Application.Commands;
using WeatherApi.Application.Queries;
using WeatherApi.Domain.Entities;
using System.Threading.Tasks;

namespace WeatherApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ISender _sender;

        public WeatherController(ISender sender)
        {
            _sender = sender;
        }

        // Create weather record
        [HttpPost("AddWeather")]
        
        public async Task<IActionResult> CreateAsync([FromBody] Weather weather)
        {
            if (weather == null)
            {
                return BadRequest("Weather data is required.");
            }

            var result = await _sender.Send(new AddWeatherCommand(weather));

            // Check if the result is null (weather wasn't added successfully)
            if (result == null)
            {
                return BadRequest("Failed to add weather.");
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }


        // Fetch weather by city
        [HttpGet("AddWeatherByCity")]
        public async Task<IActionResult> FetchWeatherAsync([FromQuery] string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City name is required.");
            }

            try
            {
                var result = await _sender.Send(new FetchWeatherCommand(city));
                if (result == null)
                {
                    return NotFound("No weather data found for the specified city.");
                }
                return Ok(result);
            }
            catch (HttpRequestException)
            {
                return NotFound("Failed to fetch weather data for the city.");
            }
        }

        // Get all weather records
        [HttpGet("GetAllWeathers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _sender.Send(new GetAllWeathersQuery());
            return Ok(result);
        }

        // Get weather by ID
        [HttpGet("GetWeatherById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var result = await _sender.Send(new GetWeatherByIdQuery(id));
            if (result == null)
            {
                return NotFound("Weather not found.");
            }
            return Ok(result);
        }

        // Update weather data
        [HttpPut("UpdateWeather")]
        public async Task<IActionResult> UpdateAsync([FromQuery] int id, [FromBody] Weather weather)
        {
            if (id <= 0 || weather == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _sender.Send(new UpdateWeatherCommand(id, weather));
            if (result == 0)
            {
                return NotFound("Weather not found.");
            }
            return NoContent();
        }

        // Delete weather record
        [HttpDelete("DeleteWeather")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var result = await _sender.Send(new DeleteWeatherCommand(id));
            if (result == 0)
            {
                return NotFound("Weather not found.");
            }
            return NoContent();
        }
    }
}
