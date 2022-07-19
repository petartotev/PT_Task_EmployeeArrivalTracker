using Microsoft.AspNetCore.Mvc;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly Random _random = new();

        [HttpPost]
        public IActionResult CreateReports([FromBody] IEnumerable<Report> request)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetReports()
        {
            var reports = new List<Report>();

            for (int i = 0; i < _random.Next(3,11); i++)
            {
                var randomInt = _random.Next(0, 1001);

                reports.Add(new Report
                {
                    EmployeeId = randomInt,
                    When = DateTime.Now.AddDays(-randomInt)
                });
            }

            return Ok(reports);
        }
    }
}