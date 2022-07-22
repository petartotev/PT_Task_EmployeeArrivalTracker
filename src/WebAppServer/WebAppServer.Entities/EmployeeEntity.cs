using Dapperer;

namespace WebAppServer.Entities;

[Table("Employees")]
public class EmployeeEntity : BaseEntity
{
    [Column("FirstName")]
    public string FirstName { get; set; }

    [Column("LastName")]
    public string LastName { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [Column("DateBirth")]
    public DateTime DateBirth { get; set; }

    [Column("ManagerId")]
    public int? ManagerId { get; set; }
    public EmployeeEntity Manager { get; set; }

    [Column("RoleId")]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }

    public IList<TeamEntity> Teams { get; set; }

    public IList<ArrivalEntity> Arrivals { get; set; }
}
