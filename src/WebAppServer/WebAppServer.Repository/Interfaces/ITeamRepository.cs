using Dapperer;
using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface ITeamRepository : IRepository, IRepository<TeamEntity, int>
{
    Task<int> CreateAsync(string name);

    Task<TeamEntity> GetByNameAsync(string name);
}
