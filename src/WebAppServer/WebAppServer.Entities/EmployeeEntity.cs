namespace WebAppServer.Entities;

public class EmployeeEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime DateBirth { get; set; }

    public int? ManagerId { get; set; }

    public int RoleId { get; set; }
}