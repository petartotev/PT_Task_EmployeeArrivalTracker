using WebAppServer.Domain.Models;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public static class ArrivalRequestMapper
{
    public static ArrivalRequestDomainModel ToDomainModel(this ArrivalRequestContract contract)
    {
        return new ArrivalRequestDomainModel
        {
            FromDate = contract.FromDate ??= DateTime.Today,
            ToDate = contract.ToDate == null ? DateTime.Today.AddDays(1).AddSeconds(-1) : contract.ToDate.Value.AddDays(1).AddSeconds(-1),
            Order = contract.Order == null ? "DESC" : (contract.Order.ToUpper() == "ASC" ? "ASC" : "DESC"),
            Skip = contract.Skip ??= 0,
            Take = contract.Take ??= 50
        };
    }
}
