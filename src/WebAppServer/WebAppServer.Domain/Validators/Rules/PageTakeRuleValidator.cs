using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class PageTakeRuleValidator : AbstractValidator<int?>, IPageTakeRuleValidator
{
    public PageTakeRuleValidator()
    {
        RuleFor(take => take)
            .Must(take => take > 0)
            .When(take => take != null)
            .WithName("'Take'")
            .WithMessage(ValidatorConstants.ErrorMessage.IntegerMustBePositiveOrEmpty);
    }
}
