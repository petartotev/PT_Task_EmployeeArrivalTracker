using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebAppServer.Api.Extensions;
using WebAppServer.Api.Filters;
using WebAppServer.Api.Swagger;
using WebAppServer.Api.Swagger.Examples.Reports;
using WebAppServer.Common;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.V1.Contracts;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;
    private readonly ISubscriptionHandler _subscriptionService;

    public ReportsController(IReportsService reportsService, ISubscriptionHandler subscriptionService)
    {
        _reportsService = reportsService;
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    [FourthTokenHeaderRequiredFilter]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(SwaggerConstants.Reports.CreateReports.Summary, SwaggerConstants.Reports.CreateReports.Description)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error[]))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ReportsCreateReportsBadRequestResponseExample))]
    public async Task<IActionResult> CreateReports([FromBody] IEnumerable<ReportContract> request)
    {
        // TODO: Create a middleware to validate [FromBody] params for null.
        if (request == null)
        {
            return BadRequest(ErrorCode.BadRequest.AsError(ErrorMessage.RequestBodyIsRequired));
        }

        if (!_subscriptionService.ValidateIncomingToken(Request.Headers[Header.WebAppServer.FourthToken]))
        {
            return Unauthorized(ErrorCode.UnauthorizedAccess.AsError(ErrorMessage.ProvidedTokenIsInvalid));
        }

        await _reportsService.CreateReportsAsync(request);

        return Ok();
    }
}
