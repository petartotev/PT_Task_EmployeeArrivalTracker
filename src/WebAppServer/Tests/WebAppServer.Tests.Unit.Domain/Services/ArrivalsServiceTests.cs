using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;
using Moq;
using WebAppServer.Domain.Services;
using WebAppServer.Entities;
using WebAppServer.Repository.Interfaces;
using WebAppServer.Tests.Infrastructure.Factories;
using WebAppServer.V1.Contracts;

namespace WebAppServer.Tests.Unit.Domain.Services;

public class ArrivalsServiceTests
{
    private readonly Mock<IRequestValidator> _requestValidator = new();
    private readonly Mock<IValidator<ArrivalRequestContract>> _arrivalRequestValidator = new();
    private readonly Mock<IArrivalRepository> _arrivalRepository = new();

    private readonly ArrivalsService _sut;

    public ArrivalsServiceTests()
    {
        _sut = new ArrivalsService(
            _requestValidator.Object,
            _arrivalRequestValidator.Object,
            _arrivalRepository.Object);
    }

    [Test]
    public async Task WithEmployeeNotFoundInDatabaseById_ThrowsException()
    {
        // Arrange
        var request = new ArrivalRequestContract
        {
            Skip = 0,
            Take = 55,
            Order = "DESC",
            FromDate = DateTime.Now,
            ToDate = DateTime.Now
        };

        // TODO: Use Factory!
        var responseFromRepo = new Dapperer.Page<ArrivalEntity>
        {
            TotalItems = 1,
            TotalPages = 1,
            CurrentPage = 1,
            ItemsPerPage = 55,
            Items = new()
        };

        _arrivalRepository
            .Setup(x => x.GetAllAsync(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
            .ReturnsAsync(responseFromRepo);

        // Act
        var result = await _sut.GetArrivalsAsync(request);

        // Assert
        var expectedResponse = Factory.Arrival.Response.Create(responsePage => responsePage.ItemsPerPage = 55);

        result.Should().BeEquivalentTo(expectedResponse);

        Console.WriteLine("TEST");
    }
}
