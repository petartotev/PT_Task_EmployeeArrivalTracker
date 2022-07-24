using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Validators;

public class ArrivalRequestValidator : AbstractValidator<ArrivalRequestContract>
{
    public ArrivalRequestValidator(
        IPageDateRuleValidator pageDateRuleValidator,
        IPageOrderRuleValidator pageOrderRuleValidator,
        IPageSkipRuleValidator pageSkipRuleValidator,
        IPageTakeRuleValidator pageTakeRuleValidator)
    {
        RuleFor(request => request.FromDate).SetValidator(pageDateRuleValidator);
        RuleFor(request => request.ToDate).SetValidator(pageDateRuleValidator);
        RuleFor(request => request.Order).SetValidator(pageOrderRuleValidator);
        RuleFor(request => request.Skip).SetValidator(pageSkipRuleValidator);
        RuleFor(request => request.Take).SetValidator(pageTakeRuleValidator);

        RuleFor(request => request)
            .Must(request => request.FromDate <= request.ToDate)
            .When(request => request.FromDate != null && request.ToDate != null)
            .WithMessage(string.Format(
                ValidatorConstants.ErrorMessage.DateFromProvidedMustBeBeforeDateTo,
                nameof(ArrivalRequestContract.FromDate),
                nameof(ArrivalRequestContract.ToDate)));
    }
}
