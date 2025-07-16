using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobsWithHangFire.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //immediatly
            // BackgroundJob.Enqueue(()=>SendMessages( "raqya18200@gmail.com"));
            // Console.WriteLine(DateTime.Now);
            //  BackgroundJob.Schedule(() => SendMessages("raqya18200@gmail.com"),TimeSpan.FromMinutes(1));


            //every month intro 
            //RecurringJob.AddOrUpdate(() => SendMessages("raqya18200@gmail.com"), Cron.Monthly(1));

            //at least call it once

            RecurringJob.AddOrUpdate(() => SendMessages("raqya18200@gmail.com"), Cron.Minutely);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        public void SendMessages(string Email)
        {
            Console.WriteLine($"Email sent at {DateTime.Now}");
        }




    }
}
