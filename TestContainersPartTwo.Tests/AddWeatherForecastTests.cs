using FluentAssertions;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using TestContainersPartTwo.Api.Weather;
using TestContainersPartTwo.Tests.Shared;
using Xunit;

namespace TestContainersPartTwo.Tests;
public class AddWeatherForecastTests : IClassFixture<DbFixture>
{
    private readonly DbFixture dbFixture;

    public AddWeatherForecastTests(DbFixture dbFixture)
    {
        this.dbFixture = dbFixture;
    }

    [Fact]
    public async Task CanSaveAndRetrieveWeatherForecasts()
    {
        var factory = new CustomWebApplicationFactory<Program>(services =>
        {
            services.SetupDatabaseConnection(dbFixture.DatabaseConnectionString);
        });

        var client = factory.CreateClient();

        CreateRandomSamplingOfWeatherForecastsInTheDatabase();

        var forecast = new AddWeatherForecastRequest
        {
            City = "New York",
            Summary = "Sunny",
            TemperatureC = 30
        };

        HttpContent c = new StringContent(JsonSerializer.Serialize(forecast), Encoding.UTF8, "application/json");
        await client.PostAsync("/weatherforecast", c);

        var response = (await client.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://localhost:7087/weatherforecast"),
            Content = new StringContent(JsonSerializer.Serialize(new WeatherForecast { City = "New York" }), Encoding.UTF8, "application/json")
        })).Content.ReadAsStringAsync().Result;

        var getResponse = JsonSerializer.Deserialize<GetWeatherForecastResponse>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        getResponse.WeatherForecasts.Should().HaveCount(1);
        getResponse.WeatherForecasts.Should().ContainSingle(x =>
            x.TemperatureC == 30
            && x.Summary == "Sunny");
    }

    private void CreateRandomSamplingOfWeatherForecastsInTheDatabase()
    {
        var insertSQL = new StringBuilder();
        for (int i = 0; i < 25; i++)
        {
            insertSQL.AppendLine("INSERT INTO WeatherForecasts ([City],[TemperatureC],[CreatedDate]" +
                ",[Summary]) " +
                $" VALUES('TestCity-{Randomizer.GetRandomString(5)}',{i}, GETUTCDATE(), 'Test-{Randomizer.GetRandomString(5)}');");
        }

        using (SqlConnection connection = new SqlConnection(
               dbFixture.DatabaseConnectionString))
        {
            SqlCommand command = new SqlCommand(insertSQL.ToString(), connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
