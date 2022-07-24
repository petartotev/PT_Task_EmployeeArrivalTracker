using WebAppServer.Domain.Validators.Rules;
using WebAppServer.Tests.Infrastructure.Constants;

namespace WebAppServer.Tests.Unit.Domain.Validators.Rules;

public class PageOrderRuleValidatorTests
{
    private readonly PageOrderRuleValidator _sut;

    public PageOrderRuleValidatorTests()
    {
        _sut = new();
    }

    [Test]
    public async Task WithValidValue_ReturnsSuccess(
        [Values("Asc", "ASC", "asc", "Desc", "DeSC", "DESC", "desc")] string order)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(order);

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Count.Should().Be(0);
    }

    [Test]
    public async Task WithNegativeValue_ReturnsInvalidResult(
        [Values("ascending", "descending", "invalid", "none")] string order)
    {
        // Arrange & Act
        var validationResult = await _sut.ValidateAsync(order);

        // Assert
        validationResult.IsValid.Should().BeFalse();
        var error = validationResult.Errors.Single();
        error.ErrorMessage
            .Should()
            .Be(string.Format(TestsConstants.ErrorMessages.RequestValidation.OrderMustBeAscDescOrEmpty, "'Order'"));
    }
}
