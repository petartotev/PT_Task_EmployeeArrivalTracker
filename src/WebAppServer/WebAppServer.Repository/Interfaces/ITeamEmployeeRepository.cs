namespace WebAppServer.Repository.Interfaces;

public interface ITeamEmployeeRepository : IRepository
{
    Task CreateAsync(int employeeId, string team);
}
