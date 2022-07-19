using Newtonsoft.Json;
using System.Net;
using WebAppServer.Api.Exceptions;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
                httpStatusCode = (HttpStatusCode)apiException.StatusCode;

                if (ex is WebAppApiException webAppApiException)
                {
                    errorCode = webAppApiException.ErrorCode;
                }
            }

            var errors = new List<Error> { new Error(errorCode, errorMessage) };

            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errors));
        }
    }
}
