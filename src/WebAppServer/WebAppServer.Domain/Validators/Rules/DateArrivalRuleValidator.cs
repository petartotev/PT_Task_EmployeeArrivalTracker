using FluentValidation;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Validators.Rules.Interfaces;

namespace WebAppServer.Domain.Validators.Rules;

public class DateArrivalRuleValidator : AbstractValidator<DateTime>, IDateArrivalRuleValidator
{
    public DateArrivalRuleValidator()
    {
        var dateNow = DateTime.Now;

        RuleFor(dateArrival => dateArrival)
            .Must(dateArrival => 
                dateArrival.Year == dateNow.Year && 
                dateArrival.Month == dateNow.Month && 
                dateArrival.Day == dateNow.Day)
            .WithName("'When'")
            .WithMessage(ValidatorConstants.ErrorMessage.DateProvidedMustBeToday);
    }
}
