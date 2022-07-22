namespace WebAppServer.V1.Contracts;

public class ArrivalRequestContract : PageableRequestContract
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Order { get; set; }
}
