using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetAll();
        void Save(WeatherForecast weatherForecast);
    }
}
