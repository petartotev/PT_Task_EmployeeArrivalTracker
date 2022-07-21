using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services.Interfaces;

public interface IReportsService
{
    Task CreateReportsAsync(IEnumerable<ReportContract> request);
}
