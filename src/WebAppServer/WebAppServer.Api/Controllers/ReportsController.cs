using Microsoft.AspNetCore.Mvc;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly Random _random = new();
    private readonly IReportsService _reportsService;
    private readonly ISubscriptionHandler _subscriptionHandler;
    public ReportsController(IReportsService reportsService, ISubscriptionHandler subscriptionHandler)
    {
        _reportsService = reportsService;
        _subscriptionHandler = subscriptionHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReports([FromBody] IEnumerable<ReportContract> request)
    {            
        if (!_subscriptionHandler.ValidateToken(Request.Headers["X-Fourth-Token"]))
        {
            return Unauthorized();
        }

        await _reportsService.CreateReportsAsync(request);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetReports()
    {
        var reports = new List<ReportContract>();

        for (int i = 0; i < _random.Next(3,11); i++)
        {
            var randomInt = _random.Next(0, 1001);

            reports.Add(new ReportContract
            {
                EmployeeId = randomInt,
                When = DateTime.Now.AddDays(-randomInt)
            });
        }

        return Ok(reports);
    }
}
