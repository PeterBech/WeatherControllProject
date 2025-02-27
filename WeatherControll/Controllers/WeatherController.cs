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
        var apiUrl = _configuration["MyWeatherAPI:BaseUrl"]; // Din lokale API URL
        string city = "Copenhagen";
        string url = $"{apiUrl}/Weather/{city}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<Rootobject>(jsonString);
            return View(weatherData);
        }

        return View("Error");
    }
}


