using System.Net;
using WebAppServer.Api.Exceptions;
using WebAppServer.Common.Helpers;
using WebAppServer.Domain.Exceptions;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;
        var errorCode = ErrorCode.UnexpectedError;
        var errorMessage = ex.Message;

        var innerException = ex.InnerException;

        while (innerException != null)
        {
            errorMessage += " " + innerException.Message;
            innerException = innerException.InnerException;
        }

        if (ex is ApiException apiException)
        {
            httpStatusCode = apiException.HttpStatusCode;
            errorCode = apiException.Code;
        }
        else if (ex is ValidatorAppException validatorAppException)
        {
            await WriteResponseAsync(context, HttpStatusCode.BadRequest, validatorAppException.Errors.Select(x => new Error(x.Code, x.Message)));
            return;
        }
        
        var errors = new List<Error> { new Error(errorCode, errorMessage) };

        await WriteResponseAsync(context, httpStatusCode, errors);
    }

    private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode httpStatusCode, IEnumerable<Error> errors)
    {
        context.Response.StatusCode = (int)httpStatusCode;

        await context.Response.WriteAsync(Json.Serialize(errors));
    }
}
