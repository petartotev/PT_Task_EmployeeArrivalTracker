using Microsoft.AspNetCore.Mvc.Testing;
using WebAppServer.Tests.Infrastructure.Asserts;

namespace WebAppServer.Tests.Infrastructure.Tests;

public abstract class BaseTests
{
    public BaseTests()
    {
        Client = new WebApplicationFactory<Program>().CreateClient();
    }

    public HttpClient Client { get; set; }

    public Asserter Assert { get; set; } = new ();
}
