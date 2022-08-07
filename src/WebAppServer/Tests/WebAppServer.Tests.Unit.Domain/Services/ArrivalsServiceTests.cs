using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;
using Moq;
using WebAppServer.Domain.Services;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Tests.Infrastructure.Factories;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Tests.Unit.Domain.Services;

public class ArrivalsServiceTests
{
    private readonly Mock<IRequestValidator> _requestValidator = new();
    private readonly Mock<IValidator<ArrivalRequestContract>> _arrivalRequestValidator = new();
    private readonly Mock<IDbContext> _dbContext = new();
    private readonly Mock<IArrivalRepository> _arrivalRepo = new();

    private readonly ArrivalsService _sut;

    public ArrivalsServiceTests()
    {
        _sut = new ArrivalsService(
            _requestValidator.Object,
            _arrivalRequestValidator.Object,
            _dbContext.Object);
    }

    [Test]
    public async Task WithEmployeeNotFoundInDatabaseById_ThrowsException()
    {
        // Arrange
        var requestToService = Factory.Arrivals.Contracts.Request.Create();
        var responseFromRepo = Factory.Arrivals.Repos.Response.Create();

        _dbContext.Setup(x => x.ArrivalRepo).Returns(_arrivalRepo.Object);
        _arrivalRepo
            .Setup(x => x.GetAllAsync(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
            .ReturnsAsync(responseFromRepo);

        // Act
        var result = await _sut.GetArrivalsAsync(requestToService);

        // Assert
        var expectedResponse = Factory.Arrivals.Contracts.Response.Create(responsePage => responsePage.ItemsPerPage = 55);

        result.Should().BeEquivalentTo(expectedResponse);
    }
}
