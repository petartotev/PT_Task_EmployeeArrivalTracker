namespace WebAppServer.V1.Contracts;

public class EmployeeContract
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime DateBirth { get; set; }

    public RoleContract Role { get; set; }
}
