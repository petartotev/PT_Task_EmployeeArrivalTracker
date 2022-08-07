namespace WebAppServer.Repository.Interfaces;

public interface IDbContext
{
    public IArrivalRepository ArrivalRepo { get; }
    
    public IEmployeeRepository EmployeeRepo { get; }

    public IRoleRepository RoleRepo { get; }

    public ITeamEmployeeRepository TeamEmployeeRepo { get; }

    public ITeamRepository TeamRepo { get; }
}
