using Dapperer;

namespace WebAppServer.Entities;

[Table("Roles")]
public class RoleEntity : BaseEntity
{
    [Column("Name")]
    public string Name { get; set; }

    public IList<EmployeeEntity> Employees { get; set; }
}
