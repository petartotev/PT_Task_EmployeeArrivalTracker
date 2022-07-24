using WebAppServer.Domain.Validators.Rules;
using WebAppServer.Tests.Infrastructure.Constants;

namespace WebAppServer.Tests.Unit.Domain.Validators.Rules;

public class PageSkipRuleValidatorTests
{
    private readonly PageSkipRuleValidator _sut;

    public PageSkipRuleValidatorTests()
    {
        _sut = new();
    }

    [Test]
    public async Task WithValidValue_ReturnsSuccess([Values(0, 1, 100, 359, int.MaxValue)] int skip)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(skip);

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithNegativeValue_ReturnsInvalidResult([Values(-1, -100, -359, -int.MaxValue)] int skip)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(skip);

        // Assert
        validationResult.IsValid.Should().BeFalse();
        var error = validationResult.Errors.Single();
        error.ErrorMessage
            .Should()
            .Be(string.Format(TestsConstants.ErrorMessages.RequestValidation.IntegerMustBeNonNegativeOrEmpty, "'Skip'"));
    }
}
