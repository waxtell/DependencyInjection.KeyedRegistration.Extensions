using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // This is most assuredly a horrible example.  Please understand this code is intended
        // solely to demonstrate the setup and use of the package.

        // The following block of code utilizes the native IServiceProvider with the
        // GetService extension that accepts an instance name (key)
        private readonly IServiceProvider _serviceProvider;

        public WeatherForecastController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IWeatherForecast Get()
        {
            var zipCodes = new[] { 44067, 44143, 44144 };

            var rng = new Random();

            return _serviceProvider.GetService<int,IWeatherForecast>(zipCodes[rng.Next(0, 3)]);
        }
/*
        // The following block of code demonstrates the use of a resolver.  Please be sure to
        // register your resolver!

        private readonly Func<int, IWeatherForecast> _resolver;

        public WeatherForecastController(Func<int, IWeatherForecast> resolver)
        {
            _resolver = resolver;
        }

        [HttpGet]
        public IWeatherForecast Get()
        {
            var zipCodes = new[] { 44067, 44143, 44144 };

            var rng = new Random();

            return _resolver.Invoke(zipCodes[rng.Next(0, 3)]);
        }
*/
    }
}
