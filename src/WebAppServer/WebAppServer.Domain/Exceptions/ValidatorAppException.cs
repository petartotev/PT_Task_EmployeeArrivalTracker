using WebAppServer.Common;
using WebAppServer.Common.Exceptions;

namespace WebAppServer.Domain.Exceptions;

public class ValidatorAppException : AppException
{
    public ValidatorAppException(IEnumerable<AppError> errors)
        :base(errors)
    {
    }

    public ValidatorAppException(params AppError[] errors)
    : base(errors)
    {
    }
}
