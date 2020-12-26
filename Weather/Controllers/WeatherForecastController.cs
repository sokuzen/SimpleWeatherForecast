using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Data;
using Weather.Models;

namespace Weather.Controllers
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
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IWeatherForecastRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        public void Post()
        {
            var rng = new Random();

            var all = _repository.GetAll();
            
            var latestDate = DateTime.Now;
            if (all.Count() > 0) latestDate = all.Max(f => f.Date);
                        
            var nextForecast = new WeatherForecast
            {
                Date = latestDate.AddDays(1),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            _repository.Save(nextForecast);
        }
    }

}
