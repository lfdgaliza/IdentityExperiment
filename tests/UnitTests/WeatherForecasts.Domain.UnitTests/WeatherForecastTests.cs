using FluentAssertions;

namespace WeatherForecasts.Domain.UnitTests;

public class WeatherForecastTests
{
    [Theory]
    [InlineData(15, 58)]
    [InlineData(-2, 29)]
    [InlineData(0, 32)]
    public void TemperatureF_WhenGet_ShouldConvertFromCelsius(int temperatureC, int expectedTemperatureF)
    {
        // Arrange
        var weatherForecast = new WeatherForecast(default, temperatureC, default);

        // Act
        var temperatureF = weatherForecast.TemperatureF;

        // Assert
        temperatureF.Should().Be(expectedTemperatureF);
    }
}