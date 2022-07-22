namespace WebAppServer.Domain.Models;

public class ArrivalDomainModel
{
    public int Id { get; set; }

    public EmployeeDomainModel Employee { get; set; }

    public DateTime DateArrival { get; set; }
}
