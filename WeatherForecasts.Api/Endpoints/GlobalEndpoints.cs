using WeatherForecasts.Api.Endpoints.WeatherForecasts;

namespace WeatherForecasts.Api.Endpoints;

/// <summary>
/// Concentrates all the endpoints in the application
/// </summary>
public static class EndpointRegistry
{
    /// <summary>
    /// Maps all the endpoints in the application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="app"></param>
    /// <returns></returns>
    public static T MapEndpoints<T>(this T app) where T : IEndpointRouteBuilder
    {
        // Notice the separation in groups. For this project I'm using a single group, but it's possible to have multiple groups.
        app.MapForecastsEndpoints();
        return app;
    }
}
