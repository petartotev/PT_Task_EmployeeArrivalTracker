using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface IRoleRepository : IRepository
{
    Task<int> CreateAsync(string name);

    Task<RoleEntity> GetByNameAsync(string name);
}
