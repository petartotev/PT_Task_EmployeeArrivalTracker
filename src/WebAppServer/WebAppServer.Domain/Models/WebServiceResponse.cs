namespace WebAppServer.Domain.Models;

public class WebServiceResponse
{
    public string Token { get; set; }

    public DateTime Expires { get; set; }
}
