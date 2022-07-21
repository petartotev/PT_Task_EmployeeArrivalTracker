namespace WebAppServer.Repository.Interfaces;

public interface IRepository
{
    Task<int> GetCountAsync();
}
