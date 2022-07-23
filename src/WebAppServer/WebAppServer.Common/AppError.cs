namespace WebAppServer.Common;

public class AppError
{
    public AppError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }

    public string Message { get; set; }
}
