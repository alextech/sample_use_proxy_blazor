using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace sample_use_proxy.Data
{
    public class WeatherContext : DbContext
    {

        public DbSet<WeatherForecast> Forecasts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                // .UseInMemoryDatabase("WeatherDB")
                .UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Weather;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var weatherEntity = modelBuilder
                .Entity<WeatherForecast>();

            weatherEntity
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();
            weatherEntity
                .Ignore(w => w.TemperatureF);

            var descriptionEntity = modelBuilder
                .Entity<Description>();
            descriptionEntity
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            weatherEntity.HasOne(w => w.Description);


            #region Seed

            var rng = new Random();

            modelBuilder.Entity<Description>()
                .HasData(Enumerable.Range(1, Summaries.Length).Select(index => new
                {
                    Id = index,
                    Summary = Summaries[index - 1]
                }));

            object[] weatherForecasts = Enumerable.Range(1, 5).Select(index => new
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                DescriptionId = rng.Next(Summaries.Length)
            }).ToArray();

            modelBuilder.Entity<WeatherForecast>()
                .HasData(weatherForecasts);

            #endregion
        }

        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}