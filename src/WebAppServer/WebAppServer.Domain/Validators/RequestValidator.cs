using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;
using WebAppServer.Common;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Exceptions;

namespace Bede.Cashier.V2.Application.Common.Validation;

public class RequestValidator : IRequestValidator
{
    public void Validate<T>(IValidator<T> validator, T objectToValidate)
    {
        var resultValidation = validator.Validate(objectToValidate);

        if (!resultValidation.IsValid)
        {
            var errors = resultValidation.Errors
                .Select(validationFailure => new AppError(ValidatorConstants.ErrorCode.ValidationError, validationFailure.ErrorMessage))
                .ToArray();

            throw new ValidatorAppException(errors);
        }
    }
}
