using FluentAssertions;
using WeatherForecasts.Application.Queries;

namespace WeatherForecasts.Application.UnitTests.Queries;
public class GetWeatherForecastRequestTests
{
    [Fact]
    public async Task Handle_WhenGet_ShouldReturnWeatherForecast()
    {
        // Arrange
        var request = new GetWeatherForecastRequest();
        var handler = new GetWeatherForecastRequestHandler();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.Count().Should().Be(GetWeatherForecastRequestHandler.ForecastCount);
    }
}
