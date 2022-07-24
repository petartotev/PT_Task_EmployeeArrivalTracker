using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class PageDateRuleValidator : AbstractValidator<DateTime?>, IPageDateRuleValidator
{
    public PageDateRuleValidator()
    {
        var dateNow = DateTime.Now;

        RuleFor(date => date)
            .Must(date => date.Value.Date <= dateNow.Date)
            .When(date => date != null)
            .WithName("'toDate' and 'fromDate'")
            .WithMessage(ValidatorConstants.ErrorMessage.DateProvidedMustBeTodayOrInThePast);
    }
}
