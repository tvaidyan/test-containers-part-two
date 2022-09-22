using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestContainersPartTwo.Api.Weather;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetWeatherForecastResponse>> GetWeatherForecast(GetWeatherForecastRequest request)
    {
        return await mediator.Send(request);
    }

    [HttpPost]
    public async Task<IActionResult> PostWeatherForecast(AddWeatherForecastRequest request)
    {
        await mediator.Send(request);
        return new OkResult();
    }

    [HttpPut]
    public async Task<IActionResult> PutWeatherForecast(UpdateWeatherForecastRequest request)
    {
        await mediator.Send(request);
        return new OkResult();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWeatherForecast(DeleteWeatherForecastRequest request)
    {
        await mediator.Send(request);
        return new OkResult();
    }
}
