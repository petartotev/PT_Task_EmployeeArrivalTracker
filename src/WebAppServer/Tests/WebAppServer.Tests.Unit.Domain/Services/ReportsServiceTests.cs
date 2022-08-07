using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;
using Moq;
using WebAppServer.Common;
using WebAppServer.Domain.Exceptions;
using WebAppServer.Domain.Services;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Tests.Infrastructure.Constants;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Tests.Unit.Domain.Services;

public class ReportsServiceTests
{
    private readonly Mock<IRequestValidator> _requestValidator = new();
    private readonly Mock<IValidator<ReportContract>> _reportRequestValidator = new();
    private readonly Mock<IDbContext> _dbContext = new();
    private readonly Mock<IEmployeeRepository> _employeeRepo = new();

    private readonly ReportsService _sut;

    public ReportsServiceTests()
    {
        _sut = new ReportsService(
            _requestValidator.Object,
            _reportRequestValidator.Object,
            _dbContext.Object);
    }

    [Test]
    public async Task WithEmployeeNotFoundInDatabaseById_ThrowsException()
    {
        // Arrange
        _dbContext.Setup(x => x.EmployeeRepo).Returns(_employeeRepo.Object);
        _employeeRepo
            .Setup(x => x.GetSingleOrDefaultAsync(It.IsAny<int>()))
            .ThrowsAsync(new ValidatorAppException(
                new AppError(TestsConstants.ErrorCodes.ValidationError,
                TestsConstants.ErrorMessages.BusinessValidation.EntityWithIdNotFoundInDatabase)));

        // Act & Assert
        await _sut
            .Invoking(x => x.CreateReportsAsync(new List<ReportContract> { new ReportContract { EmployeeId = 1, When = DateTime.Now } }))
            .Should()
            .ThrowAsync<ValidatorAppException>();
    }
}
