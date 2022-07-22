using Dapperer;

namespace WebAppServer.Entities;

[Table("Teams")]
public class TeamEntity : BaseEntity
{
    [Column("Name")]
    public string Name { get; set; }

    public IList<EmployeeEntity> Employees { get; set; }
}
