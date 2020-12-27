using System.Collections.Generic;
using System.Linq;
using Weather.Models;

namespace Weather.Data
{
    public class DbWeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WeatherForecastDbContext _dbContext;
        public DbWeatherForecastRepository(WeatherForecastDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _dbContext.Forecasts.AsEnumerable();
        }

        public void Save(WeatherForecast weatherForecast)
        {
            _dbContext.Add(weatherForecast);
            _dbContext.SaveChanges();
        }
    }
}
