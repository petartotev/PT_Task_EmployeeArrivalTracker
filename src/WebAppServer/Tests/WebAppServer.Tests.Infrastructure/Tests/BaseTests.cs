using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAppServer.Tests.Infrastructure.Tests;

public class BaseTests
{
    public BaseTests()
    {
        Client = new WebApplicationFactory<Program>().CreateClient();
    }

    public HttpClient Client { get; set; }

    public Asserts.Assert Assert { get; set; } = new ();
}
