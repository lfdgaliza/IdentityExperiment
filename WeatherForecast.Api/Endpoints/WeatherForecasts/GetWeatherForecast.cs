using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Endpoints.WeatherForecasts;

public static class GetWeatherForecast
{
    public static async Task<IResult> Handler([FromServices] IMediator mediator)
    {
        var forecast = await mediator.Send(new GetWeatherForecastRequest());
        return Results.Ok(forecast);
    }
}