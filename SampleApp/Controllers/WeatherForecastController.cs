using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Func<int, IWeatherForecast> _resolver;

        public WeatherForecastController(Func<int, IWeatherForecast> resolver, ILogger<WeatherForecastController> logger)
        {
            _resolver = resolver;
            _logger = logger;
        }

        [HttpGet]
        public IWeatherForecast Get()
        {
            var zipCodes = new[] {44067, 44143, 44144};

            var rng = new Random();

            return _resolver.Invoke(zipCodes[rng.Next(0, 3)]);
        }
    }
}
