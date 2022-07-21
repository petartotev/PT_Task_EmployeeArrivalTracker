namespace WebAppServer.Repository.Interfaces;

public interface IArrivalRepository : IRepository
{
    Task CreateAsync(int employeeId, DateTime dateTime);
}
