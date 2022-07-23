namespace WebAppServer.Common.Exceptions;

public class AppException : Exception
{
    public AppException(string code, string message)
        :this(new AppError(code, message))
    {
    }

    public AppException(IEnumerable<AppError> errors)
        :base(string.Empty)
    {
        Errors = errors;
    }

    public AppException(params AppError[] errors)
    : base(string.Empty)
    {
        Errors = errors;
    }

    public IEnumerable<AppError> Errors { get; }
}
