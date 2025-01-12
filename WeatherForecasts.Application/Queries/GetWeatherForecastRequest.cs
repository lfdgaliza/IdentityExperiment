using MediatR;
using WeatherForecasts.Domain;

namespace WeatherForecasts.Application.Queries;

public sealed record GetWeatherForecastRequest : IRequest<IEnumerable<WeatherForecast>>;

internal sealed class GetWeatherForecastRequestHandler : IRequestHandler<GetWeatherForecastRequest, IEnumerable<Domain.WeatherForecast>>
{
    public Task<IEnumerable<Domain.WeatherForecast>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable
            .Range(1, 5)
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