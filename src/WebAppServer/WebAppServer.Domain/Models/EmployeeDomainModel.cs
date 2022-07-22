namespace WebAppServer.Domain.Models;

public class EmployeeDomainModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime DateBirth { get; set; }

    public RoleDomainModel Role { get; set; }
}
