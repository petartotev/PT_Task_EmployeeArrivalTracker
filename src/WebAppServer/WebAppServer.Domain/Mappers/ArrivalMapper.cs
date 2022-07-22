using WebAppServer.Domain.Models;
using WebAppServer.Entities;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public static class ArrivalMapper
{
    public static ArrivalContract ToContract(this ArrivalDomainModel arrivalDomainModel)
    {
        return new ArrivalContract
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

    public static ArrivalDomainModel ToDomainModel(this ArrivalContract arrivalContract)
    {
        return new ArrivalDomainModel
        {
            Id = arrivalContract.Id,
            DateArrival = arrivalContract.DateArrival,
            Employee = arrivalContract.Employee.ToDomainModel()
        };
    }
}
