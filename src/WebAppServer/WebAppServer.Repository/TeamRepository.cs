using System.Data.SqlClient;
using Dapper;
using Dapperer;
using Serilog;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Common.Constants;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class TeamRepository : Repository<TeamEntity, int>, ITeamRepository
{
    private readonly IDbSettings _settings;

    public TeamRepository(IQueryBuilder queryBuilder, IDbFactory dbFactory, IDbSettings settings)
        : base(queryBuilder, dbFactory)
    {
        _settings = settings;
    }

    public async Task<int> CreateAsync(string name)
    {
        var sql = $"INSERT INTO [dbo].[Teams] ([Name]) VALUES(N'{name}')";

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
                    Log.Error(string.Format(LoggerMessages.Database.FailedToCreateEntity, nameof(TeamEntity)) + " " + ex.Message);
                }
            }
        }

        return (await GetByNameAsync(name)).Id;
    }

    public async Task<TeamEntity> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [dbo].[Teams] WHERE [Id] = '{id}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<TeamEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<TeamEntity> GetByNameAsync(string name)
    {
        var sql = $"SELECT * FROM [dbo].[Teams] WHERE [Name] = '{name}'";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<TeamEntity>(sql)).SingleOrDefault();
        }
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[Teams]";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
