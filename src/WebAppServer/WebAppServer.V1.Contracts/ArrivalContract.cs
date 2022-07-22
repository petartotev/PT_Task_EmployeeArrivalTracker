namespace WebAppServer.V1.Contracts;

public class ArrivalContract
{
    public int Id { get; set; }

    public DateTime DateArrival { get; set; }

    public EmployeeContract Employee { get; set; }
}
