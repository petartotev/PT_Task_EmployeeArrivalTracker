using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class DbContext : IDbContext
{
    public DbContext(
        IArrivalRepository arrivalRepo,
        IEmployeeRepository employeeRepo,
        IRoleRepository roleRepo,
        ITeamEmployeeRepository teamEmployeeRepo,
        ITeamRepository teamRepo)
    {
        ArrivalRepo = arrivalRepo;
        EmployeeRepo = employeeRepo;
        RoleRepo = roleRepo;
        TeamEmployeeRepo = teamEmployeeRepo;
        TeamRepo = teamRepo;
    }

    public IArrivalRepository ArrivalRepo { get; }

    public IEmployeeRepository EmployeeRepo { get; }

    public IRoleRepository RoleRepo { get; }

    public ITeamEmployeeRepository TeamEmployeeRepo { get; }

    public ITeamRepository TeamRepo { get; }
}
