using System.Net;
using FluentAssertions;

namespace WebAppServer.Tests.Infrastructure.Asserts;

public class Assert
{
    public void ResponseIsSuccess(
        HttpResponseMessage response,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        response.IsSuccessStatusCode.Should().Be(true);
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    public void ResponseIsFail(
        HttpResponseMessage response,
        HttpStatusCode expectedStatusCode = HttpStatusCode.BadRequest)
    {
        response.IsSuccessStatusCode.Should().Be(false);
        response.StatusCode.Should().Be(expectedStatusCode);
    }
}
