using Dapperer;
using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface IRoleRepository : IRepository, IRepository<RoleEntity, int>
{
    Task<int> CreateAsync(string name);

    Task<RoleEntity> GetByNameAsync(string name);
}
