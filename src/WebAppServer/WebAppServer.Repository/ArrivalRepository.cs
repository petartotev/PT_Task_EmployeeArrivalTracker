using Dapper;
using Serilog;
using System.Data.SqlClient;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class ArrivalRepository : IArrivalRepository
{
    private readonly string _connectionString;

    public ArrivalRepository(IDbSettings settings)
    {
        _connectionString = settings.ConnectionString;
    }

    public async Task CreateAsync(int employeeId, DateTime dateTime)
    {
        var sql = $"INSERT INTO [dbo].[Arrivals] " +
            $"([EmployeeId], [DateArrival]) VALUES(" +
            $"N'{employeeId}', N'{dateTime}')";

        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                int rowsAffected = await connection.ExecuteAsync(sql);

                Log.Information($"Report was added: {dateTime} | {employeeId}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[Arrivals]";

        using (var connection = new SqlConnection(_connectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
