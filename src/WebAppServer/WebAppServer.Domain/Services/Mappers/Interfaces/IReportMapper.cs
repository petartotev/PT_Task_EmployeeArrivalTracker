using WebAppServer.Domain.Services.Models;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services.Mappers.Interfaces;

public interface IReportMapper
{
    IEnumerable<ReportServiceModel> Map(IEnumerable<ReportContract> collectionReportsContract);
}
