using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Data
{
    public class DefaultWeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly DefaultWeatherForecastDataContext _dataContext;

        public DefaultWeatherForecastRepository(DefaultWeatherForecastDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<WeatherForecast> GetAll()
        {
            return _dataContext.Forecasts;
        }

        public void Save(WeatherForecast weatherForecast)
        {
            _dataContext.Forecasts = _dataContext.Forecasts.Append(weatherForecast);
        }
    }
}
