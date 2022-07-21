namespace WebAppServer.Repository.Seeder.Models;

public class Employee
{
    public int Id { get; set; }

    public int? ManagerId { get; set; }

    public int Age { get; set; }

    public List<string> Teams { get; set; }

    public string Role { get; set; }

    public string Email { get; set; }

    public string SurName { get; set; }

    public string Name { get; set; }
}
