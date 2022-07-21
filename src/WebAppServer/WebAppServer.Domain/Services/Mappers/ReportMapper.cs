using WebAppServer.Domain.Services.Mappers.Interfaces;
using WebAppServer.Domain.Services.Models;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services.Mappers;

public class ReportMapper : IReportMapper
{
    public IEnumerable<ReportServiceModel> Map(IEnumerable<ReportContract> collectionReportsContract)
    {
        return collectionReportsContract.Select(contract => new ReportServiceModel
        {
            EmployeeId = contract.EmployeeId,
            DateArrival = contract.When
        });
    }
}
