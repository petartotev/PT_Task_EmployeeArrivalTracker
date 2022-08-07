using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapperer;
using Serilog;
using WebAppServer.Common.Constants;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class ArrivalRepository : Repository<ArrivalEntity, int>, IArrivalRepository
{
    private readonly IDappererSettings _settings;

    public ArrivalRepository(IQueryBuilder queryBuilder, IDbFactory dbFactory, IDappererSettings settings)
        : base(queryBuilder, dbFactory)
    {
        _settings = settings;
    }

    public async Task CreateAsync(int employeeId, DateTime dateTime)
    {
        var sql = $"INSERT INTO [dbo].[Arrivals] ([EmployeeId], [DateArrival]) VALUES(N'{employeeId}', N'{dateTime}')";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            try
            {
                await connection.ExecuteAsync(sql);

                Log.Information(string.Format(LoggerMessages.Database.EntityCreated, nameof(ArrivalEntity)) + $" {dateTime} | {employeeId}.");
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(LoggerMessages.Database.FailedToCreateEntity, nameof(ArrivalEntity)) + " " + (ex.Message));
            }
        }
    }

    public async Task<Page<ArrivalEntity>> GetAllAsync(
        DateTime fromDate,
        DateTime toDate,
        string order = "DESC",
        int skip = 0,
        int take = 50)
    {
        if (fromDate > toDate || (order.ToUpperInvariant() != "ASC" && order.ToUpperInvariant() != "DESC"))
        {
            throw new Exception();
        }

        var sqlCount = $@"
            SELECT COUNT ([Id]) FROM [dbo].[Arrivals]
            WHERE [DateArrival] > '{fromDate}' AND [DateArrival] < '{toDate}'";

        var sql = $@"
            SELECT * FROM [dbo].[Arrivals]
            WHERE [DateArrival] > '{fromDate}' AND [DateArrival] < '{toDate}'
            ORDER BY [DateArrival] {order.ToUpperInvariant()}
            OFFSET {skip} ROWS
            FETCH NEXT {take} ROWS ONLY";

        using IDbConnection connection = CreateConnection();

        var totalCount = connection.Query<int>(sqlCount).SingleOrDefault();

        var result = (await connection.QueryAsync<ArrivalEntity>(sql)).ToList();

        await PopulateOneToOneAsync(x => x.EmployeeId, y => y.Employee, result);
        await PopulateOneToOneAsync(x => x.Employee.RoleId, y => y.Employee.Role, result);

        return PageResults(skip, take, totalCount, result);
    }

    public async Task<int> GetCountAsync()
    {
        var sql = $"SELECT COUNT([Id]) FROM [dbo].[Arrivals]";

        using (var connection = new SqlConnection(_settings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
