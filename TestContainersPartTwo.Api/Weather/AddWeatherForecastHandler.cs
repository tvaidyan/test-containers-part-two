using Dapper;
using MediatR;
using TestContainersPartTwo.Api.Shared.DataAccess;

namespace TestContainersPartTwo.Api.Weather;
public class AddWeatherForecastRequest : IRequest<Unit>
{
    public string City { get; set; } = string.Empty;
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
}

public class AddWeatherForecastHandler : IRequestHandler<AddWeatherForecastRequest, Unit>
{
    private readonly IDatabase database;

    public AddWeatherForecastHandler(IDatabase database)
    {
        this.database = database;
    }

    public async Task<Unit> Handle(AddWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@city", request.City);
        parameters.Add("@temperatureC", request.TemperatureC);
        parameters.Add("@summary", request.Summary);

        await database.ExecuteFileAsync("Weather/insert-weather-forecast.sql", parameters);

        return Unit.Value;
    }
}