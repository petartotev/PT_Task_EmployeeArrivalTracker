namespace WebAppServer.Domain.Models;

public abstract class PageableRequestDomainModel
{
    public int Skip { get; set; }

    public int Take { get; set; }
}
