using Dapperer;

namespace WebAppServer.Entities;

[Table("Arrivals")]
public class ArrivalEntity : BaseEntity
{
    [Column("DateArrival")]
    public DateTime DateArrival { get; set; }

    [Column("EmployeeId")]
    public int EmployeeId { get; set; }

    public EmployeeEntity Employee { get; set; }
}
