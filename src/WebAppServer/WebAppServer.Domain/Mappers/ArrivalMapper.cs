using WebAppServer.Domain.Models;
using WebAppServer.Entities;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public static class ArrivalMapper
{
    public static ArrivalResponseContract ToContract(this ArrivalDomainModel arrivalDomainModel)
    {
        return new ArrivalResponseContract
        {
            Id = arrivalDomainModel.Id,
            DateArrival = arrivalDomainModel.DateArrival,
            Employee = arrivalDomainModel.Employee.ToContract()
        };
    }

    public static ArrivalDomainModel ToDomainModel(this ArrivalEntity arrivalEntity)
    {
        return new ArrivalDomainModel
        {
            Id = arrivalEntity.Id,
            DateArrival = arrivalEntity.DateArrival,
            Employee = arrivalEntity.Employee.ToDomainModel()
        };
    }

    public static ArrivalDomainModel ToDomainModel(this ArrivalResponseContract arrivalContract)
    {
        return new ArrivalDomainModel
        {
            Id = arrivalContract.Id,
            DateArrival = arrivalContract.DateArrival,
            Employee = arrivalContract.Employee.ToDomainModel()
        };
    }
}
