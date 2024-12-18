namespace WeatherApi.Infrastructure.Models
{
    public class WeatherApiResponse
    {
        public string Name { get; set; }  // City name
        public SysInfo Sys { get; set; }  // Country info
        public MainInfo Main { get; set; }  // Weather data like temperature
    }

    public class SysInfo
    {
        public string Country { get; set; }  // Country
    }

    public class MainInfo
    {
        public double? Temp { get; set; }  // Temperature (nullable in case it's missing)
    }
}
