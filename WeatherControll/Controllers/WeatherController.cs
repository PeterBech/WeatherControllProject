using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherControll.Models;

public class WeatherController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherController(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        // Replace with your real API URL and include your API key
        var apiKey = _configuration["OpenWeather:ApiKey"];
        var baseUrl = _configuration["OpenWeather:BaseUrl"];
        var city = "Copenhagen";
        string url = $"{baseUrl}?q={city}&appid={apiKey}&units=metric";

        // Send the HTTP request
        var response = await _httpClient.GetAsync(url);

        // Ensure it was successful
        response.EnsureSuccessStatusCode();

        // Read response as a string
        var jsonString = await response.Content.ReadAsStringAsync();

        // Deserialize JSON to your WeatherData model
        Rootobject weatherData = JsonSerializer.Deserialize<Rootobject>(jsonString);

        // Pass the deserialized object to the view
        return View(weatherData);
    }
}

