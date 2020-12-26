using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Data
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetAll();
        void Save(WeatherForecast weatherForecast);
    }
}
