using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class PageSkipRuleValidator : AbstractValidator<int?>, IPageSkipRuleValidator
{
    public PageSkipRuleValidator()
    {
        RuleFor(skip => skip)
            .Must(skip => skip >= 0)
            .When(skip => skip != null)
            .WithName("'Skip'")
            .WithMessage(ValidatorConstants.ErrorMessage.IntegerMustBeNonNegativeOrEmpty);
    }
}
