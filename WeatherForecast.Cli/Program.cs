using Ardalis.GuardClauses;
using IdentityModel.Client;
using System.Text.Json;

await Task.Delay(1000);

var client = new HttpClient();
var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if(discovery.IsError)
{
    Console.WriteLine(discovery.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest 
{
    Address = discovery.TokenEndpoint,
    ClientId = "my-weather-app",
    ClientSecret = "my-super-secret",
    Scope = "weather-forecast"
}, CancellationToken.None);

if(tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);

var weatherForecastApiClient = new HttpClient();
weatherForecastApiClient.SetBearerToken(Guard.Against.Null(tokenResponse.AccessToken));
var response = await weatherForecastApiClient.GetAsync("https://localhost:6001/identity");

if(response.IsSuccessStatusCode)
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
    Console.WriteLine(JsonSerializer.Serialize(value: doc,
                                               options: new JsonSerializerOptions { WriteIndented = true }));

    Console.ReadKey();
    return;
}
Console.WriteLine(response.StatusCode);