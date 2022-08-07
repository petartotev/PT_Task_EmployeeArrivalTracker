using System.Net;
using FluentAssertions;
using WebAppServer.Common.Helpers;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Tests.Infrastructure.Asserts;

public class AsserterResponse
{
    public void IsSuccess(
    HttpResponseMessage response,
    HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        response.IsSuccessStatusCode.Should().Be(true);
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    public async Task IsFailAsync(
        HttpResponseMessage response,
        HttpStatusCode expectedStatusCode = HttpStatusCode.BadRequest,
        params Error[] expectedErrors)
    {
        response.IsSuccessStatusCode.Should().Be(false);
        response.StatusCode.Should().Be(expectedStatusCode);
        var result = await response.Content.ReadAsStringAsync();
        var actualErrors = Json.Deserialize<Error[]>(result);
        actualErrors.Should().BeEquivalentTo(expectedErrors);
    }
}
