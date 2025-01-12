using Ardalis.GuardClauses;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WeatherForecast.Web.Pages
{
    public class WeatherForecastModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        public string Json { get; set; } = string.Empty;
        public async Task OnGet()
        {
            // TODO: Client name should be in a constant
            var weatherForecastApiClient = httpClientFactory.CreateClient("weather-forecast-api");

            var response = await weatherForecastApiClient.GetAsync("weatherforecast");

            if (response.IsSuccessStatusCode)
            {
                var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                Json = JsonSerializer.Serialize(value: doc,
                                                options: new JsonSerializerOptions { WriteIndented = true });
            }
        }
    }
}
