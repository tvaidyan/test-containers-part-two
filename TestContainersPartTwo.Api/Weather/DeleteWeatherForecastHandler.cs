using Dapper;
using MediatR;
using TestContainersPartTwo.Api.Shared.DataAccess;

namespace TestContainersPartTwo.Api.Weather;
public class DeleteWeatherForecastRequest : IRequest<Unit>
{
    public string City { get; set; } = string.Empty;
}

public class DeleteWeatherForecastHandler : IRequestHandler<DeleteWeatherForecastRequest, Unit>
{
    private readonly IDatabase database;

    public DeleteWeatherForecastHandler(IDatabase database)
    {
        this.database = database;
    }

    public async Task<Unit> Handle(DeleteWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@city", request.City);

        await database.ExecuteFileAsync("Weather/delete-weather-forecast.sql", parameters);

        return Unit.Value;
    }
}