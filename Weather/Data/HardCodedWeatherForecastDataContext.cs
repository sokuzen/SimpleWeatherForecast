using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Data
{
    public class HardCodedWeatherForecastDataContext
    {
        private IQueryable<WeatherForecast> _forecasts;

        public IQueryable<WeatherForecast> Forecasts
        {
            get
            {
                if (this._forecasts == null) this._forecasts = Enumerable.Empty<WeatherForecast>().AsQueryable();
                return this._forecasts;
            }
            set
            {
                this._forecasts = value;
            }
        }
    }
}
