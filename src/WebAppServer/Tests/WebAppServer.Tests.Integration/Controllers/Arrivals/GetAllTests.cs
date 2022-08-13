using Microsoft.AspNetCore.WebUtilities;
using WebAppServer.Api.Extensions;
using WebAppServer.Tests.Infrastructure.Tests;
using WebAppServer.V1.Contracts.Common;
using static WebAppServer.Tests.Infrastructure.Constants.TestsConstants;

namespace WebAppServer.Tests.Integration.Controllers.Arrivals;

public class GetAllTests : BaseTests
{
    // ==================== Positive Scenarios ====================
    [Test]
    public async Task WithNoUrlParams_ReturnsOk()
    {
        // Arrange & Act
        var response = await Client.GetAsync(GetUrl(null, null, null, null, null));

        // Assert
        Assert.Response.IsSuccess(response);
    }

    [Test]
    public async Task WithAllParamsUsedAndValid_ReturnsOk()
    {
        // Arrange
        var fromDate = DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd");
        var toDate = DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd");

        // Act
        var response = await Client.GetAsync(GetUrl(fromDate, toDate, "ASC", "0", "50"));
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Response.IsSuccess(response);
    }

    // ==================== Negative Scenarios ====================
    [Test]
    public async Task WithUrlParamToDateInTheFuture_ReturnsBadRequest()
    {
        // Arrange
        var fromDate = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
        var toDate = DateTime.Today.AddDays(2).ToString("yyyy-MM-dd");

        // Act
        var response = await Client.GetAsync(GetUrl(fromDate, toDate, null, null, null));

        // Assert
        await Assert.Response.IsFailAsync(
            response,
            System.Net.HttpStatusCode.BadRequest,
            new Error[]
            {
                ErrorCodes.ValidationError
                .AsError(string.Format(ErrorMessages.RequestValidation.DateProvidedMustBeTodayOrInThePast, "'toDate' and 'fromDate'"))
            });
            
    }

    [Test]
    public async Task WithUrlParamFromDateLaterThanUrlParamToDate_ReturnsBadRequest()
    {
        // Arrange
        var fromDate = DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd");
        var toDate = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");

        // Act
        var response = await Client.GetAsync(GetUrl(fromDate, toDate, null, null, null));

        // Assert
        await Assert.Response.IsFailAsync(
            response,
            System.Net.HttpStatusCode.BadRequest,
            new Error[]
            {
                ErrorCodes.ValidationError
                .AsError(string.Format(ErrorMessages.RequestValidation.DateFromProvidedMustBeBeforeDateTo, "FromDate", "ToDate"))
            });
    }

    // ==================== Helper Methods ====================
    private static Uri GetUrl(string fromDate, string toDate, string order, string skip, string take)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "fromDate", fromDate },
            { "toDate", toDate },
            { "order", order },
            { "skip", skip },
            { "take", take }
        };

        return new Uri(QueryHelpers.AddQueryString("http://localhost:5168/api/arrivals", queryParams));
    }
}
