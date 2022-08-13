using System.Net;
using System.Text;
using Newtonsoft.Json;
using WebAppServer.Api.Extensions;
using WebAppServer.Tests.Infrastructure.Tests;
using WebAppServer.V1.Contracts;
using WebAppServer.V1.Contracts.Common;
using static WebAppServer.Tests.Infrastructure.Constants.TestsConstants;

namespace WebAppServer.Tests.Integration.Controllers.Reports;

public class CreateReportsTests : BaseTests
{
    // ==================== Negative Scenarios ====================
    [Test]
    public async Task WithMissingFourthTokenHeader_ReturnsForbidden(
        [Values(null, "")] string fourthTokenHeader)
    {
        // Arrange
        var reports = new ReportContract[]
        {
            new ReportContract
            {
                EmployeeId = 1,
                When = DateTime.Now.AddHours(-1)
            }
        };
        var httpRequestMessage = GetHttpRequestMessage(fourthTokenHeader, reports);

        // Act
        var response = await Client.SendAsync(httpRequestMessage);

        // Assert
        await Assert.Response.IsFailAsync(
            response,
            HttpStatusCode.Forbidden,
            new Error[]
            {
                ErrorCodes.BadRequest.AsError(ErrorMessages.RequestValidation.FourthTokenIsARequiredHeader)
            });
    }

    [Test]
    public async Task WithInvalidFourthTokenHeader_ReturnsForbidden(
    [Values(" ", "5d3b5321-597e-46bd-825f-41e76df41045")] string fourthTokenHeader)
    {
        // Arrange
        var reports = new ReportContract[]
        {
            new ReportContract
            {
                EmployeeId = 1,
                When = DateTime.Now.AddHours(-1)
            }
        };
        var httpRequestMessage = GetHttpRequestMessage(fourthTokenHeader, reports);

        // Act
        var response = await Client.SendAsync(httpRequestMessage);

        // Assert
        await Assert.Response.IsFailAsync(
            response,
            HttpStatusCode.Unauthorized,
            ErrorCodes.UnauthorizedAccess.AsError(ErrorMessages.RequestValidation.ProvidedTokenIsInvalid));
    }

    // ==================== Helper Methods ====================
    private static HttpRequestMessage GetHttpRequestMessage(
        string fourthTokenHeaderValue = null,
        ReportContract[] reports = null,
        Action<HttpRequestMessage> setup = null)
    {
        var content = JsonConvert.SerializeObject(reports);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = GetUrl(),
            Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), "application/json" },
                { "X-Fourth-Token", fourthTokenHeaderValue }
            },
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        setup?.Invoke(request);

        return request;
    }

    private static Uri GetUrl()
    {
        return new Uri("http://localhost:5168/api/reports");
    }
}
