using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WeatherForecasts.Application.Queries;

namespace WeatherForecasts.Api.Endpoints.WeatherForecasts;

public static class GetWeatherForecast
{
    public static async Task<Ok<GetWeatherForecastResponse>> Handler([FromServices] IMediator mediator)
    {
        var forecasts = await mediator.Send(new GetWeatherForecastRequest());
        var responseModel = new GetWeatherForecastResponse(forecasts);
        return TypedResults.Ok(responseModel);
    }
}

public sealed class GetWeatherForecastResponse
{
    internal GetWeatherForecastResponse(IEnumerable<Domain.WeatherForecast> forecastList)
    {
        Forecasts = forecastList
            .Select(forecast => new WeatherForecast(Date: forecast.Date,
                                                    TemperatureC: forecast.TemperatureC,
                                                    TemperatureF: forecast.TemperatureF,
                                                    Summary: forecast.Summary)).ToArray();
    }

    public WeatherForecast[] Forecasts { get; set; }
}

public sealed record WeatherForecast(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);