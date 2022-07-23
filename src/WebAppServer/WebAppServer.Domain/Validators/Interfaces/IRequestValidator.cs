using FluentValidation;

namespace Bede.Cashier.V2.Application.Common.Validation.Interfaces;

public interface IRequestValidator
{
    void Validate<T>(IValidator<T> validator, T objectToValidate);
}
