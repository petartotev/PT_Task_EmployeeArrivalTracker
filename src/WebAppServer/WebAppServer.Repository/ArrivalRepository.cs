using Dapper;
using Dapperer;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;

namespace WebAppServer.Repository;

public class ArrivalRepository : Repository<ArrivalEntity, int>, IArrivalRepository
{
    private readonly IQueryBuilder _queryBuilder;
    private readonly IDbFactory _dbFactory;
    private readonly IDbSettings _dbSettings;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IRoleRepository _roleRepo;

    public ArrivalRepository(
        IQueryBuilder queryBuilder,
        IDbFactory dbFactory,
        IDbSettings dbSettings,
        IEmployeeRepository employeeRepo,
        IRoleRepository roleRepo)
        : base(queryBuilder, dbFactory)
    {
        _queryBuilder = queryBuilder;
        _dbFactory = dbFactory;
        _dbSettings = dbSettings;
        _employeeRepo = employeeRepo;
        _roleRepo = roleRepo;
    }

    public async Task CreateAsync(int employeeId, DateTime dateTime)
    {
        var sql = $"INSERT INTO [dbo].[Arrivals] " +
            $"([EmployeeId], [DateArrival]) VALUES(" +
            $"N'{employeeId}', N'{dateTime}')";

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
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

    public async Task<Page<ArrivalEntity>> GetAllAsync(DateTime fromDate, DateTime toDate, string order, int skip = 0, int take = 50)
    {
        if (fromDate > toDate)
        {
            throw new Exception();
        }

        if (order != "ASC" && order != "DESC")
        {
            throw new Exception();
        }

        var sqlCount = $@"
            SELECT COUNT ([Id]) FROM [dbo].[Arrivals]
            WHERE [DateArrival] > '{fromDate}' AND [DateArrival] < '{toDate}'";

        var sql = $@"
            SELECT * FROM [dbo].[Arrivals]
            WHERE [DateArrival] > '{fromDate}' AND [DateArrival] < '{toDate}'
            ORDER BY [DateArrival] {order}
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

        using (var connection = new SqlConnection(_dbSettings.ConnectionString))
        {
            return (await connection.QueryAsync<int>(sql)).SingleOrDefault();
        }
    }
}
