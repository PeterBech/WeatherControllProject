using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherControll.Models;

namespace MyWeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var apiKey = _configuration["OpenWeather:ApiKey"];
            var baseUrl = _configuration["OpenWeather:BaseUrl"];

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(baseUrl))
            {
                return BadRequest("API-nøglen eller base URL mangler i appsettings.json.");
            }

            string url = $"{baseUrl}?q={city}&appid={apiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Kunne ikke hente vejrdata.");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<Rootobject>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Intern serverfejl: {ex.Message}");
            }
        }


    //    public async Task<IActionResult> Index()
    //    {
    //        // Replace with your real API URL and include your API key
    //        var apiKey = _configuration["OpenWeather:ApiKey"];
    //        var baseUrl = _configuration["OpenWeather:BaseUrl"];
    //        var city = "Copenhagen";
    //        string url = $"{baseUrl}?q={city}&appid={apiKey}&units=metric";

    //        // Send the HTTP request
    //        var response = await _httpClient.GetAsync(url);

    //        // Ensure it was successful
    //        response.EnsureSuccessStatusCode();

    //        // Read response as a string
    //        var jsonString = await response.Content.ReadAsStringAsync();

    //        // Deserialize JSON to your WeatherData model
    //        Rootobject weatherData = JsonSerializer.Deserialize<Rootobject>(jsonString);

    //        // Pass the deserialized object to the view
    //        return View(weatherData);
    //    }
    }
}
