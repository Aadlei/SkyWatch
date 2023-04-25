using Microsoft.AspNetCore.Mvc;
using MudBlazor.Extensions;
using System.Net;
using System.Text.Json;

namespace SkyWatch.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherController> _logger;


        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        // Web Api GET based on latitude and longitude
        [HttpGet("{lat}&{lon}")]
        public async Task<IActionResult?> Get(int lat, int lon)
        {
            WeatherData? weatherData;

            // Sets up URL and modifies it by the selected values
            var baseURL = "https://api.met.no/weatherapi/locationforecast/2.0/compact?";
            var modifiedURL = baseURL + "lat=" + lat + "&lon=" + lon;

            WebClient wb = new WebClient();                                 // Web Client
            wb.Headers.Add("User-Agent: Other");                            // Important to avoid "403: Forbidden"
            string json = wb.DownloadString(modifiedURL);                   // Json string gathered from the modified url
            weatherData = JsonSerializer.Deserialize<WeatherData>(json);    // Deserialize process combined with our model

            return Ok(weatherData);

        }
    }
}          
            
        
            
