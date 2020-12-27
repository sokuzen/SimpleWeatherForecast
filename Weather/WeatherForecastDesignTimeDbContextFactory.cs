using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Weather.Data;

namespace Weather
{
    public class WeatherForecastDesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherForecastDbContext>
    {
        public WeatherForecastDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var dbContextBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration
                        .GetConnectionString("SqlConnectionString");

            dbContextBuilder.UseSqlServer(connectionString);

            return new WeatherForecastDbContext(dbContextBuilder.Options);
        }
    }
}
