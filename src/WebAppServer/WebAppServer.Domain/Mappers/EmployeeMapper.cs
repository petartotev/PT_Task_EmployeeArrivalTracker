using WebAppServer.Domain.Models;
using WebAppServer.Entities;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public static class EmployeeMapper
{
    public static EmployeeContract ToContract(this EmployeeDomainModel employeeDomainModel)
    {
        return new EmployeeContract
        {
            Id = employeeDomainModel.Id,
            FirstName = employeeDomainModel.FirstName,
            LastName = employeeDomainModel.LastName,
            Email = employeeDomainModel.Email,
            DateBirth = employeeDomainModel.DateBirth,
            Role = employeeDomainModel.Role.ToContract()
        };
    }

    public static EmployeeDomainModel ToDomainModel(this EmployeeEntity employeeEntity)
    {
        return new EmployeeDomainModel
        {
            Id = employeeEntity.Id,
            FirstName = employeeEntity.FirstName,
            LastName = employeeEntity.LastName,
            Email = employeeEntity.Email,
            DateBirth = employeeEntity.DateBirth,
            Role = employeeEntity.Role.ToDomainModel()
        };
    }

    public static EmployeeDomainModel ToDomainModel(this EmployeeContract employeeContract)
    {
        return new EmployeeDomainModel
        {
            Id = employeeContract.Id,
            FirstName = employeeContract.FirstName,
            LastName = employeeContract.LastName,
            Email = employeeContract.Email,
            DateBirth = employeeContract.DateBirth,
            Role = employeeContract.Role.ToDomainModel()
        };
    }
}
