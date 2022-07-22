using Dapperer;

namespace WebAppServer.Entities;

[Table("TeamsEmployees")]
public class TeamsEmployeesEntity : BaseEntity
{
    [Column("TeamId")]
    public int TeamId { get; set; }

    [Column("EmployeeId")]
    public int EmployeeId { get; set; }
}
