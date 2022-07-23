
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Extensions;

public static class ErrorCodeExtensions
{
    public static Error[] ToErrorResponse(this string errorCode, string errorMessage)
    {
        return new[] { errorCode.AsError(errorMessage) };
    }

    public static Error AsError(this string errorCode, string errorMessage)
    {
        return new Error(errorCode, errorMessage);
    }
}
