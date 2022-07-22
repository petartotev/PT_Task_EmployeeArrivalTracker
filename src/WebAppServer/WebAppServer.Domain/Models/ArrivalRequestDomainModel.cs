namespace WebAppServer.Domain.Models;

public class ArrivalRequestDomainModel : PageableRequestDomainModel
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Order { get; set; }
}
