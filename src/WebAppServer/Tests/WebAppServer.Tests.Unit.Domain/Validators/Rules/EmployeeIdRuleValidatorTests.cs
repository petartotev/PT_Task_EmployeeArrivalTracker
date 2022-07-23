using WebAppServer.Domain.Validators.Rules;
using WebAppServer.Tests.Infrastructure.Constants;

namespace WebAppServer.Tests.Unit.Domain.Validators.Rules;

public class EmployeeIdRuleValidatorTests
{
    private readonly EmployeeIdRuleValidator _sut;

    public EmployeeIdRuleValidatorTests()
    {
        _sut = new();
    }

    [Test]
    public async Task WithValidValue_ReturnsSuccess([Values(1, 100, 359, int.MaxValue)] int employeeId)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(employeeId);

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithNonPositiveValue_ReturnsInvalidResult([Values(0, -1, -100, -359, -int.MaxValue)] int employeeId)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(employeeId);

        // Assert
        validationResult.IsValid.Should().BeFalse();
        var error = validationResult.Errors.Single();
        error.ErrorMessage
            .Should()
            .Be(string.Format(TestsConstants.ErrorMessages.RequestValidation.IntegerMustBePositive, "'EmployeeId'"));
    }
}
