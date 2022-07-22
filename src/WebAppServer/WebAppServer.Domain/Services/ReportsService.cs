using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IArrivalRepository _arrivalRepository;

    public ReportsService(IArrivalRepository arrivalRepository)
    {
        _arrivalRepository = arrivalRepository;
    }

    public async Task CreateReportsAsync(IEnumerable<ReportContract> request)
    {
        // TODO: Bulk insert instead of foreach!
        foreach (var report in request)
        {
            await _arrivalRepository.CreateAsync(report.EmployeeId, report.When);
        }
    }
}
