using FluentValidation;
using WebAppServer.Domain.Validators.Rules.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Validators;

public class ReportRequestValidator : AbstractValidator<ReportContract>
{
    public ReportRequestValidator(
        IEmployeeIdRuleValidator employeeIdRuleValidator,
        IDateArrivalRuleValidator dateArrivalRuleValidator)
    {
        RuleFor(x => x.EmployeeId).SetValidator(employeeIdRuleValidator);
        RuleFor(x => x.When).SetValidator(dateArrivalRuleValidator);
    }
}
