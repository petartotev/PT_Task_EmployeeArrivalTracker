using WebAppServer.Domain.Models;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers.Interfaces;

public interface IReportMapper
{
    IEnumerable<ReportDomainModel> Map(IEnumerable<ReportContract> collectionReportsContract);
}
