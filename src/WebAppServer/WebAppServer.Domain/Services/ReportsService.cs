using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Domain.Services.Mappers.Interfaces;
using WebAppServer.Repository.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IArrivalRepository _arrivalRepository;
    private readonly IReportMapper _mapper;

    public ReportsService(IArrivalRepository arrivalRepository, IReportMapper mapper)
    {
        _arrivalRepository = arrivalRepository;
        _mapper = mapper;
    }

    public async Task CreateReportsAsync(IEnumerable<ReportContract> request)
    {
        var model = _mapper.Map(request);

        foreach (var report in request)
        {
            // TODO: increment employeeId by 1.
            await _arrivalRepository.CreateAsync(report.EmployeeId, report.When);
        }
    }
}
