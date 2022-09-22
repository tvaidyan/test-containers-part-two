using Dapper;
using MediatR;
using TestContainersPartTwo.Api.Shared.DataAccess;

namespace TestContainersPartTwo.Api.Weather;
public class GetWeatherForecastRequest : IRequest<GetWeatherForecastResponse>
{
    public string City { get; set; } = string.Empty;
}

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, GetWeatherForecastResponse>
{
    private readonly IDatabase database;

    public GetWeatherForecastHandler(IDatabase database)
    {
        this.database = database;
    }

    public async Task<GetWeatherForecastResponse> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@city", request.City);

        var results = await database.ExecuteFileAsync<WeatherForecast>("Weather/select-weather-forecast.sql", parameters);

        return new GetWeatherForecastResponse
        {
            WeatherForecasts = results
        };
    }
}

public class GetWeatherForecastResponse
{
    public IEnumerable<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();
}