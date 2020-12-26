using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather
{
    public class HardCodedWeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly HardCodedWeatherForecastDataContext _dataContext;

        public HardCodedWeatherForecastRepository(HardCodedWeatherForecastDataContext dataContext)
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
