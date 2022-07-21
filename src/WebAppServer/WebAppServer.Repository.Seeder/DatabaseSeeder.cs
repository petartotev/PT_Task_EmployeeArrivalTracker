using Serilog;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;
using WebAppServer.Repository.Seeder.Models;

namespace WebAppServer.Repository.Seeder;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly IRoleRepository _roleRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITeamEmployeeRepository _teamEmployeeRepository;

    public DatabaseSeeder(
        IRoleRepository roleRepository,
        ITeamRepository teamRepository,
        IEmployeeRepository employeeRepository,
        ITeamEmployeeRepository teamEmployeeRepository)
    {
        _roleRepository = roleRepository;
        _teamRepository = teamRepository;
        _employeeRepository = employeeRepository;
        _teamEmployeeRepository = teamEmployeeRepository;
    }

    public async Task SeedFromFileAsync(string path)
    {
        if (await IsDatabaseEmptyAsync())
        {
            var employees = await JsonManager.GetEmployeesFromJsonFileAsync(path);

            await SeedRolesAsync(employees.Select(x => x.Role).Distinct());
            await SeedTeamsAsync(employees.SelectMany(x => x.Teams).Distinct());
            await SeedEmployeesAsync(employees);
        }
    }

    private async Task SeedRolesAsync(IEnumerable<string> roles)
    {
        Log.Information("Seeds roles...");

        foreach (var role in roles)
        {
            await _roleRepository.CreateAsync(role);
            Thread.Sleep(5);
        };
    }

    private async Task SeedTeamsAsync(IEnumerable<string> teams)
    {
        Log.Information("Seeds teams...");

        foreach (var team in teams)
        {
            await _teamRepository.CreateAsync(team);
            Thread.Sleep(5);
        };
    }

    private async Task SeedEmployeesAsync(IEnumerable<Employee> employees)
    {
        Log.Information("Seeds employees. It can take a minute...");

        foreach (var employee in employees)
        {
            var employeeDbEntityId = await _employeeRepository
                .CreateAsync(employee.Id, employee.Name, employee.SurName, employee.Email, employee.Age, employee.Role, employee.ManagerId);
            Thread.Sleep(5);

            await SeedTeamsEmployeesPerEmployeeAsync(employeeDbEntityId, employee.Teams.Distinct());
        };
    }

    private async Task SeedTeamsEmployeesPerEmployeeAsync(int employeeId, IEnumerable<string> teams)
    {
        foreach (var team in teams)
        {
            await _teamEmployeeRepository.CreateAsync(employeeId, team);
            Thread.Sleep(5);
        }
    }

    private async Task<bool> IsDatabaseEmptyAsync()
    {
        return 
            await _teamEmployeeRepository.GetCountAsync() == 0 &&
            await _employeeRepository.GetCountAsync() == 0 &&
            await _teamRepository.GetCountAsync() == 0 &&
            await _roleRepository.GetCountAsync() == 0;
    }
}
