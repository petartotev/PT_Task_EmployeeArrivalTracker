using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class PageOrderRuleValidator : AbstractValidator<string>, IPageOrderRuleValidator
{
    public PageOrderRuleValidator()
    {
        RuleFor(order => order)
            .Must(order => order.ToUpperInvariant() == "ASC" || order.ToUpperInvariant() == "DESC")
            .When(order => order != null)
            .WithName("'Order'")
            .WithMessage(ValidatorConstants.ErrorMessage.OrderMustBeAscDescOrEmpty);
    }
}
