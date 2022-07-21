using Dapper;
using Serilog;
using System.Data.SqlClient;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IDbSettings _settings;

    public RoleRepository(IDbSettings settings)
    {
        _settings = settings;
    }

    public async Task<int> CreateAsync(string name)
    {
        var sql = $"INSERT INTO [dbo].[Roles] ([Name]) VALUES(N'{name}')";

        var entityWithTheSameName = await GetByNameAsync(name);

        if (entityWithTheSameName is null)
        {
            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    int rowsAffected = await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create role '{name}'. {ex.Message}");
                }
            }
        }

        return (await GetByNameAsync(name)).Id;
    }

    public async Task<RoleEntity> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [dbo].[Roles] WHERE [Id] = '{id}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<RoleEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<RoleEntity> GetByNameAsync(string name)
    {
        var sql = $"SELECT * FROM [dbo].[Roles] WHERE [Name] = '{name}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<RoleEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[Roles]";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
