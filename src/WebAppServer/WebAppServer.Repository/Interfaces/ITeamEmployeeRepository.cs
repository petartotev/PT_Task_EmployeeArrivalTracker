using Dapperer;
using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface ITeamEmployeeRepository : IRepository, IRepository<TeamsEmployeesEntity, int>
{
    Task CreateAsync(int employeeId, string team);
}
