using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using WebAppServer.Api.Swagger;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.V1.Contracts;
using WebAppServer.V1.Contracts.Common;

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
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error[]))]
    public async Task<IActionResult> GetAll([FromQuery] ArrivalRequestContract request)
    {
        var result = await _arrivalsService.GetArrivalsAsync(request);

        Log.Information("SENT INFORMATION SUCCESSFULLY!");

        return Ok(result);
    }
}
