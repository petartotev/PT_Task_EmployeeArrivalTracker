using System.Data.SqlClient;
using Dapper;
using Dapperer;
using Serilog;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Common.Constants;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class RoleRepository : Repository<RoleEntity, int>, IRoleRepository
{
    private readonly IDbSettings _settings;

    public RoleRepository(IQueryBuilder queryBuilder, IDbFactory dbFactory, IDbSettings settings)
        : base(queryBuilder, dbFactory)
    {
        _settings = settings;
    }

    public async Task<int> CreateAsync(string name)
    {
        var sql = $"INSERT INTO [dbo].[Roles] ([Name]) VALUES(N'{name}')";

        if (await GetByNameAsync(name) is null)
        {
            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format(LoggerMessages.Database.FailedToCreateEntity, nameof(RoleEntity)) + " " + ex.Message);
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
