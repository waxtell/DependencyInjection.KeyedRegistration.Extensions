using System;

namespace SampleApp
{
    public interface IWeatherForecast
    {
        DateTime Date { get; }
        int TemperatureC { get; }
        int TemperatureF { get; }
        string Summary { get; }
    }

    public class WeatherForecast : IWeatherForecast
    {
        public DateTime Date { get; }
        public int TemperatureC { get; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; }

        public WeatherForecast(DateTime date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }
    }
}
