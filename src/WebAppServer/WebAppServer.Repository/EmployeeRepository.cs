using System.Data.SqlClient;
using Dapper;
using Serilog;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Common.Constants;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbSettings _settings;
    private readonly IRoleRepository _roleRepository;

    public EmployeeRepository(IDbSettings settings, IRoleRepository roleRepository)
    {
        _settings = settings;
        _roleRepository = roleRepository;
    }

    public async Task<int> CreateAsync(int id, string firstName, string lastName, string email, int age, string role, int? managerId)
    {
        var sql = managerId != null
            ? $"INSERT INTO [dbo].[Employees] ([FirstName], [LastName], [Email], [DateBirth], [RoleId], [ManagerId]) VALUES(" +
              $"N'{firstName}', N'{lastName}', N'{email}', '{DateTime.Now.AddYears(-age)}', {(await _roleRepository.GetByNameAsync(role)).Id}, {managerId})"
            : $"INSERT INTO [dbo].[Employees] ([FirstName], [LastName], [Email], [DateBirth], [RoleId]) VALUES(" +
              $"N'{firstName}', N'{lastName}', N'{email}', '{DateTime.Now.AddYears(-age)}', {(await _roleRepository.GetByNameAsync(role)).Id})";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            try
            {
                await connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(LoggerMessages.Database.FailedToCreateEntity, nameof(EmployeeEntity)) + " " + ex.Message);
            }
        }

        return (await GetByEmailAsync(email)).Id;
    }

    public async Task<EmployeeEntity> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [dbo].[Employees] WHERE [Id] = '{id}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<EmployeeEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<EmployeeEntity> GetByEmailAsync(string email)
    {
        var sql = $"SELECT * FROM [dbo].[Employees] WHERE [Email] = '{email}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<EmployeeEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[Employees]";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
