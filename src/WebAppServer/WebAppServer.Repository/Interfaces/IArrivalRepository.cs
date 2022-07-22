using Dapperer;
using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface IArrivalRepository : IRepository
{
    Task CreateAsync(int employeeId, DateTime dateTime);

    Task<Page<ArrivalEntity>> GetAllAsync(DateTime fromDate, DateTime toDate, string order, int skip, int take);
}
