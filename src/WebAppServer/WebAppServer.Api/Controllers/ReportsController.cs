﻿using Microsoft.AspNetCore.Mvc;
using WebAppServer.Api.Filters;
using WebAppServer.Common;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;
    private readonly ISubscriptionHandler _subscriptionHandler;

    public ReportsController(IReportsService reportsService, ISubscriptionHandler subscriptionHandler)
    {
        _reportsService = reportsService;
        _subscriptionHandler = subscriptionHandler;
    }

    [HttpPost]
    [FourthTokenHeaderRequiredFilter]
    public async Task<IActionResult> CreateReports([FromBody] IEnumerable<ReportContract> request)
    {            
        if (!_subscriptionHandler.ValidateToken(Request.Headers[Header.FourthToken]))
        {
            return Unauthorized();
        }

        await _reportsService.CreateReportsAsync(request);

        return Ok();
    }
}
