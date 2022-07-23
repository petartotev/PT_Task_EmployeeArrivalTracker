using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using WebAppServer.Api.Swagger;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArrivalsController : ControllerBase
{
    private readonly IArrivalsService _arrivalsService;

    public ArrivalsController(IArrivalsService arrivalsService)
    {
        _arrivalsService = arrivalsService;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(SwaggerConstants.Arrivals.GetAll.Summary, SwaggerConstants.Arrivals.GetAll.Description)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] ArrivalRequestContract request)
    {
        var result = await _arrivalsService.GetArrivalsAsync(request);

        Log.Information("SENT INFORMATION SUCCESSFULLY!");

        return Ok(result);
    }
}
