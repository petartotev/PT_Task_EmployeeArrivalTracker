namespace WebAppServer.V1.Contracts.Common;

public class Error
{
    public Error()
    {
    }

    public Error(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }
}
