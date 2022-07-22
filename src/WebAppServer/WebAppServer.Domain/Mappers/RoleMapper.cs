using WebAppServer.Domain.Models;
using WebAppServer.Entities;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Mappers;

public static class RoleMapper
{
    public static RoleContract ToContract(this RoleDomainModel roleDomainModel)
    {
        return new RoleContract
        {
            Id = roleDomainModel.Id,
            Name = roleDomainModel.Name
        };
    }

    public static RoleDomainModel ToDomainModel(this RoleContract roleContract)
    {
        return new RoleDomainModel
        {
            Id = roleContract.Id,
            Name = roleContract.Name
        };
    }

    public static RoleDomainModel ToDomainModel(this RoleEntity roleEntity)
    {
        return new RoleDomainModel
        {
            Id = roleEntity.Id,
            Name = roleEntity.Name
        };
    }
}
