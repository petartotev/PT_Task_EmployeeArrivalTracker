using Dapperer;
using WebAppServer.Entities;

namespace WebAppServer.Repository.Interfaces;

public interface IEmployeeRepository : IRepository, IRepository<EmployeeEntity, int>
{
    Task<int> CreateAsync(int id, string firstName, string lastName, string email, int age, string role, int? managerId);
}
