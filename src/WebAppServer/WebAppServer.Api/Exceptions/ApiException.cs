namespace WebAppServer.Api.Exceptions;

public class ApiException : Exception
{
    public ApiException(int statusCode, string errorMessage)
        : base(errorMessage)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}
