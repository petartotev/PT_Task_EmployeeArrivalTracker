using Serilog;
using WebAppServer.Common.Constants;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;
using WebAppServer.Repository.Seeder.Models;

namespace WebAppServer.Repository.Seeder;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly Random _random = new();
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
            var employeesFromFile = await JsonManager.GetEmployeesFromJsonFileAsync(path);

            await SeedRolesAsync(employeesFromFile.Select(x => x.Role).Distinct());
            await SeedTeamsAsync(employeesFromFile.SelectMany(x => x.Teams).Distinct());
            await SeedEmployeesAsync(employeesFromFile);
        }
    }

    private async Task SeedRolesAsync(IEnumerable<string> roles)
    {
        Log.Information(LoggerMessages.Database.Seeder.SeedingRoles);

        foreach (var role in roles)
        {
            await _roleRepository.CreateAsync(role);
        };
    }

    private async Task SeedTeamsAsync(IEnumerable<string> teams)
    {
        Log.Information(LoggerMessages.Database.Seeder.SeedingTeams);

        foreach (var team in teams)
        {
            await _teamRepository.CreateAsync(team);
        };
    }

    private async Task SeedEmployeesAsync(IEnumerable<EmployeeSeederModel> employees)
    {
        Log.Warning(LoggerMessages.Database.Seeder.SeedingEmployees);

        int seedStopper = 0;

        foreach (var employee in employees)
        {
            // N.B. In the data json file Ids start from 0. The database tables identity starts from 1, so we increment here.
            // N.B. In the file there are idential emails for different employees. These emails are concatenated with id here in order to be unique.
            var employeeId = employee.Id + 1;
            var managerId = employee.ManagerId == null ? employee.ManagerId : employee.ManagerId + 1;
            var emailSplit = employee.Email.Split("@");
            var uniqueEmail = (emailSplit != null && emailSplit.Length == 2) ? (emailSplit[0] + $"{employee.Id}@" + emailSplit[1]) : employee.Id + "@.mail.com";

            seedStopper++;

            var employeeDbEntityId = await _employeeRepository
                .CreateAsync(employeeId, employee.Name, employee.SurName, employee.Email, employee.Age, employee.Role, managerId);

            await SeedTeamsEmployeesPerEmployeeAsync(employeeDbEntityId, employee.Teams.Distinct());

            if (seedStopper < 301)
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
        }
    }

    private async Task SeedArrivalsInTheLastDaysPerEmployeeAsync(int employeeId, int daysFromToday = 5)
    {
        for (int day = 1; day <= daysFromToday; day++)
        {
            await _arrivalRepository
                .CreateAsync(employeeId, DateTime.Today.AddDays(-day).AddHours(_random.Next(8, 11)).AddMinutes(_random.Next(0, 60)));
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
