namespace WeatherForecasts.Api.Endpoints.WeatherForecasts;

/// <summary>
/// Contains the endpoints for the weather forecast
/// </summary>
public static class ForecastsEndpointsRegistry
{
    /// <summary>
    /// Maps the weather forecast endpoints
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapForecastsEndpoints(this IEndpointRouteBuilder app)
    {
        var forecastEndpoints = app.MapGroup("/weatherforecast");

        // I like to separate routes from handlers, but this is a personal preference and I can adapt to the team's preference.
        forecastEndpoints
            .MapGet("/", GetWeatherForecast.Handler)
            .RequireAuthorization() // Protects the endpoint with the default authorization policy
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        return app;
    }
}