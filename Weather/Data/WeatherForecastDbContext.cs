using Microsoft.EntityFrameworkCore;
using Weather.Models;

namespace Weather.Data
{
    public class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherForecast> Forecasts { get; set; }
    }
}
