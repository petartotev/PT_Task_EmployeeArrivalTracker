using WebAppServer.Domain.Validators.Rules;
using WebAppServer.Tests.Infrastructure.Constants;

namespace WebAppServer.Tests.Unit.Domain.Validators.Rules;

public class DateArrivalRuleValidatorTests
{
    private readonly DateArrivalRuleValidator _sut;

    public DateArrivalRuleValidatorTests()
    {
        _sut = new();
    }

    [Test]
    public async Task WithDateDuringToday_ReturnsSuccess([Values(1, 1 * 60, 23 * 60, 23 * 60 + 59)] int minutesAfterMidnightTonight)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(DateTime.Today.AddMinutes(minutesAfterMidnightTonight));

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithDateASecondAfterMidnightToday_ReturnsSuccess()
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(DateTime.Today.AddSeconds(1));

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithDateASecondBeforeMidnightTomorrow_ReturnsSuccess()
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(DateTime.Today.AddDays(1).AddSeconds(-1));

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithDatesNotFromToday_ReturnsInvalidResult([Values(-365 * 10, -1, 1, - 100, 101)] int daysFromNow)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(DateTime.Now.AddDays(daysFromNow));

        // Assert
        validationResult.IsValid.Should().BeFalse();
        var error = validationResult.Errors.Single();
        error.ErrorMessage
            .Should()
            .Be(string.Format(TestsConstants.ErrorMessages.RequestValidation.DateProvidedMustBeToday, "'When'"));
    }
}
