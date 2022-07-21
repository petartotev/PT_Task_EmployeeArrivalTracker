using Dapper;
using Serilog;
using System.Data.SqlClient;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class TeamEmployeeRepository : ITeamEmployeeRepository
{
    private readonly IDbSettings _dbSettings;
    private readonly ITeamRepository _teamRepository;

    public TeamEmployeeRepository(IDbSettings dbSettings, ITeamRepository teamRepository)
    {
        _dbSettings = dbSettings;
        _teamRepository = teamRepository;
    }

    public async Task CreateAsync(int employeeId, string team)
    {
        var sql = $"INSERT INTO [dbo].[TeamsEmployees] ([EmployeeId], [TeamId]) VALUES({employeeId}, {(await _teamRepository.GetByNameAsync(team)).Id})";

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
        {
            try
            {
                int rowsAffected = await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not create team-employee '{team}'-{employeeId}. {ex.Message}");
            }
        }
    }

    public async Task<TeamsEmployeesEntity> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [dbo].[TeamsEmployees] WHERE [Id] = '{id}'";

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
        {
            return (await connection.QueryAsync<TeamsEmployeesEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<TeamsEmployeesEntity> GetByNameAsync(string name)
    {
        var sql = $"SELECT * FROM [dbo].[TeamsEmployees] WHERE [Name] = '{name}'";

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
        {
            return (await connection.QueryAsync<TeamsEmployeesEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[TeamsEmployees]";

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
