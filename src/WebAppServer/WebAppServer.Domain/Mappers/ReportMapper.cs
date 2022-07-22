using WebAppServer.Domain.Models;
using WebAppServer.Domain.Mappers.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public class ReportMapper : IReportMapper
{
    public IEnumerable<ReportDomainModel> Map(IEnumerable<ReportContract> collectionReportsContract)
    {
        return collectionReportsContract.Select(contract => new ReportDomainModel
        {
            EmployeeId = contract.EmployeeId,
            DateArrival = contract.When
        });
    }
}
