using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class EmployeeIdRuleValidator : AbstractValidator<int>, IEmployeeIdRuleValidator
{
    public EmployeeIdRuleValidator()
    {
        RuleFor(employeeId => employeeId)
            .Must(employeeId => employeeId > 0)
            .WithName("'EmployeeId'")
            .WithMessage(ValidatorConstants.ErrorMessage.IntegerMustBePositive);
    }
}
