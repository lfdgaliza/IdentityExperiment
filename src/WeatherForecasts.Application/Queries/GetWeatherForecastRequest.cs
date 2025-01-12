using MediatR;
using WeatherForecasts.Domain;

namespace WeatherForecasts.Application.Queries;

public sealed record GetWeatherForecastRequest : IRequest<IEnumerable<WeatherForecast>>;

internal sealed class GetWeatherForecastRequestHandler : IRequestHandler<GetWeatherForecastRequest, IEnumerable<WeatherForecast>>
{
    public const int ForecastCount = 5;

    public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable
            .Range(1, ForecastCount)
            .Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ));

        return Task.FromResult(forecast);
    }
}