using System.Net;

namespace WebAppServer.Api.Exceptions;

public class WebAppApiException : ApiException
{
    public WebAppApiException(HttpStatusCode statusCode, string errorCode, string errorMessage)
        : base((int)statusCode, errorMessage)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; set; }
}
