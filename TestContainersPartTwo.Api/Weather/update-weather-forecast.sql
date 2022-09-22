UPDATE WeatherForecasts SET [TemperatureC] = @temperatureC,
[Summary] = @summary
WHERE City = @city;