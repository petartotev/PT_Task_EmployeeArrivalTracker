namespace WebAppServer.V1.Contracts;

public class PageableRequestContract
{
    public int? Skip { get; set; }

    public int? Take { get; set; }
}
