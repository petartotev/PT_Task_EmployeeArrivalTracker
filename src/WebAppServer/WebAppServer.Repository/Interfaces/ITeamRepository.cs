using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface ITeamRepository : IRepository
{
    Task<int> CreateAsync(string name);

    Task<TeamEntity> GetByNameAsync(string name);
}
