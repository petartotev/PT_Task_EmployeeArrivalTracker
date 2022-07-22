using Serilog;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;
using WebAppServer.Repository.Seeder.Models;

namespace WebAppServer.Repository.Seeder;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly Random _random = new ();
    private readonly IRoleRepository _roleRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITeamEmployeeRepository _teamEmployeeRepository;
    private readonly IArrivalRepository _arrivalRepository;

    public DatabaseSeeder(
        IRoleRepository roleRepository,
        ITeamRepository teamRepository,
        IEmployeeRepository employeeRepository,
        ITeamEmployeeRepository teamEmployeeRepository,
        IArrivalRepository arrivalRepository)
    {
        _roleRepository = roleRepository;
        _teamRepository = teamRepository;
        _employeeRepository = employeeRepository;
        _teamEmployeeRepository = teamEmployeeRepository;
        _arrivalRepository = arrivalRepository;
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
            Thread.Sleep(1);
        };
    }

    private async Task SeedTeamsAsync(IEnumerable<string> teams)
    {
        Log.Information("Seeds teams...");

        foreach (var team in teams)
        {
            await _teamRepository.CreateAsync(team);
            Thread.Sleep(1);
        };
    }

    private async Task SeedEmployeesAsync(IEnumerable<EmployeeSeederModel> employees)
    {
        Log.Information("Seeds employees. It can take a few minutes...");

        int limit = 0;

        foreach (var employee in employees)
        {
            // N.B. In the data json file Ids start from 0. The database tables identity starts from 1, so we increment here.
            var employeeId = employee.Id + 1;
            var managerId = employee.ManagerId == null ? employee.ManagerId : employee.ManagerId + 1;

            limit++;

            var employeeDbEntityId = await _employeeRepository
                .CreateAsync(employeeId, employee.Name, employee.SurName, employee.Email, employee.Age, employee.Role, managerId);
            Thread.Sleep(1);

            await SeedTeamsEmployeesPerEmployeeAsync(employeeDbEntityId, employee.Teams.Distinct());

            if (limit < 501)
            {
                await SeedArrivalsInTheLastDaysPerEmployeeAsync(employeeDbEntityId);
            }
        };
    }

    private async Task SeedTeamsEmployeesPerEmployeeAsync(int employeeId, IEnumerable<string> teams)
    {
        foreach (var team in teams)
        {
            await _teamEmployeeRepository.CreateAsync(employeeId, team);
            Thread.Sleep(1);
        }
    }

    private async Task SeedArrivalsInTheLastDaysPerEmployeeAsync(int employeeId, int daysInThePastFromYesterday = 4)
    {
        for (int i = 1; i <= daysInThePastFromYesterday; i++)
        {
            await _arrivalRepository
                .CreateAsync(employeeId, DateTime.Today.AddDays(-i).AddHours(_random.Next(8, 11)).AddMinutes(_random.Next(0, 60)));
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
