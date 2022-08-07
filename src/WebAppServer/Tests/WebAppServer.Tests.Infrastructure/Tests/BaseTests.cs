using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAppServer.Tests.Infrastructure.Tests;

public class BaseTests
{
    public BaseTests()
    {
        var factory = new WebApplicationFactory<Program>();
        Client = factory.CreateClient();
    }

    public HttpClient Client { get; set; }

    public Asserts.Assert Assert { get; set; } = new ();
}
