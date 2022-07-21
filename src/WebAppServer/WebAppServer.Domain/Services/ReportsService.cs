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
        foreach (var report in request)
        {
            // TODO: increment employeeId by 1.
            await _arrivalRepository.CreateAsync(report.EmployeeId, report.When);
        }
    }
}
