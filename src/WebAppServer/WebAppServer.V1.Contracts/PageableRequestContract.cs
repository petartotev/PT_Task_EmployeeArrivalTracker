namespace WebAppServer.V1.Contracts;

public class PageableRequestContract
{
    // <summary>Number of transactions to skip from the database results before return in the response.\r\n Must be divisible by the value of Take parameter.</summary>
    /// <example>800</example>
    public int? Skip { get; set; }
    /// <summary>The number of transactions to be fetched in a single request. Max count to be fetched is 200.</summary>
    /// <example>200</example>
    public int? Take { get; set; }
}
