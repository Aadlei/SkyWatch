using Microsoft.AspNetCore.Mvc;
using MudBlazor.Extensions;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SkyWatch.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    // The Web API Controller for SkyWatch
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
            if (lat <= 90 && lon <= 180)
            {
                if (lat >= -90 && lon >= -180)
                {
                    WeatherData? weatherData;

                    // Sets up URL and modifies it by the selected values
                    var baseURL = "https://api.met.no/weatherapi/locationforecast/2.0/compact?";
                    var modifiedURL = baseURL + "lat=" + lat + "&lon=" + lon;

                    HttpClient hc = new HttpClient();                               // HTTP Client
                    hc.DefaultRequestHeaders.Add("User-Agent", "Other");             // Important to avoid "403: Forbidden"
                    string json = await hc.GetStringAsync(modifiedURL);             // Json string gathered from the modified url
                    weatherData = JsonSerializer.Deserialize<WeatherData>(json);    // Deserialize process combined with our model

                    return Ok(weatherData);

                // If the 'if statements' fail, they will return a message why they fail instead of the actual model.
                } else
                {
                    Response.StatusCode = 400;
                    return Content("Latitude cannot be less than -90 and Longitude cannot be less than -180!");
                }
            } else
            {
                Response.StatusCode = 400;
                return Content("Latitude cannot be more than 90 and Longitude cannot be more than 180!");
            }
        }
    }
}          
            
        
            
