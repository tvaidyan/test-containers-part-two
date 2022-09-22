using Dapper;
using MediatR;
using TestContainersPartTwo.Api.Shared.DataAccess;

namespace TestContainersPartTwo.Api.Weather;
public class UpdateWeatherForecastRequest : IRequest<Unit>
{
    public string City { get; set; } = string.Empty;
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
}

public class UpdateWeatherForecastHandler : IRequestHandler<UpdateWeatherForecastRequest, Unit>
{
    private readonly IDatabase database;

    public UpdateWeatherForecastHandler(IDatabase database)
    {
        this.database = database;
    }

    public async Task<Unit> Handle(UpdateWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@city", request.City);
        parameters.Add("@temperatureC", request.TemperatureC);
        parameters.Add("@summary", request.Summary);

        await database.ExecuteFileAsync("Weather/update-weather-forecast.sql", parameters);

        return Unit.Value;
    }
}