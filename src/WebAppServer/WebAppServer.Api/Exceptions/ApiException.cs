using System.Net;

namespace WebAppServer.Api.Exceptions;

public class ApiException : Exception
{
    public ApiException(HttpStatusCode httpStatusCode, string code, string message)
        : base(message)
    {
        Code = code;
        HttpStatusCode = httpStatusCode;
    }

    public string Code { get; set; }

    public HttpStatusCode HttpStatusCode { get; set; }
}
