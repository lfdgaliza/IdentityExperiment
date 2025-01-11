using MediatR;

namespace WeatherForecast.Api.Queries;

public sealed record GetWeatherForecastRequest : IRequest<IEnumerable<Forecast>>;

internal sealed record Forecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal sealed class GetWeatherForecastRequestHandler : IRequestHandler<GetWeatherForecastRequest, IEnumerable<Forecast>>
{
    public Task<IEnumerable<Forecast>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable
            .Range(1, 5)
            .Select(index =>
                new Forecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ));

        return Task.FromResult(forecast);
    }
}