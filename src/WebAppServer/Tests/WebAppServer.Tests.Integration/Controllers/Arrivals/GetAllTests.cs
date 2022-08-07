using WebAppServer.Tests.Infrastructure.Tests;

namespace WebAppServer.Tests.Integration.Controllers.Arrivals;

[TestFixture]
public class GetAllTests : BaseTests
{
    [Test]
    public async Task WithNoUrlParams_ReturnsOk()
    {
        // Arrange & Act
        var response = await Client.GetAsync("http://localhost:5168/api/arrivals");

        // Assert
        Assert.ResponseIsSuccess(response);
    }

    [Test]
    public async Task WithUrlParamsFromDateAndToDateInTheFuture_ReturnsBadRequest()
    {
        // Arrange & Act
        var response = await Client.GetAsync("http://localhost:5168/api/arrivals?fromDate=2099-07-23&toDate=2099-08-04");

        // Assert
        Assert.ResponseIsFail(response);
    }
}
