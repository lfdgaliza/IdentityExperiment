using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WeatherForecast.Web.Pages
{
    public class WeatherForecastModel : PageModel
    {
        public string Json { get; set; } = string.Empty;
        public async Task OnGet()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            // TODO: Use a factory
            var weatherForecastApiClient = new HttpClient();
            weatherForecastApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await weatherForecastApiClient.GetAsync("https://localhost:6001/weatherforecast");

            if (response.IsSuccessStatusCode)
            {
                var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                Json = JsonSerializer.Serialize(value: doc,
                                                options: new JsonSerializerOptions { WriteIndented = true });
            }
        }
    }
}
