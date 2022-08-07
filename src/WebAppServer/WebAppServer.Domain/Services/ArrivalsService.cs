using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.Interfaces;
using WebAppServer.V1.Contracts;
using WebAppServer.Domain.Mappers;
using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;

namespace WebAppServer.Domain.Services;

public class ArrivalsService : IArrivalsService
{
    private readonly IRequestValidator _requestValidator;
    private readonly IValidator<ArrivalRequestContract> _arrivalsRequestValidator;
    private readonly IDbContext _dbContext;

    public ArrivalsService(
        IRequestValidator requestValidator,
        IValidator<ArrivalRequestContract> arrivalsRequestValidator,
        IDbContext dbContext)
    {
        _requestValidator = requestValidator;
        _arrivalsRequestValidator = arrivalsRequestValidator;
        _dbContext = dbContext;
    }

    public async Task<Page<ArrivalResponseContract>> GetArrivalsAsync(ArrivalRequestContract request)
    {
        _requestValidator.Validate(_arrivalsRequestValidator, request);

        var requestDomain = request.ToDomainModel();

        var result = await _dbContext.ArrivalRepo.GetAllAsync(
            requestDomain.FromDate,
            requestDomain.ToDate,
            requestDomain.Order,
            requestDomain.Skip,
            requestDomain.Take);

        return new Page<ArrivalResponseContract>
        {
            CurrentPage = result.CurrentPage,
            ItemsPerPage = result.ItemsPerPage,
            TotalPages = result.TotalPages,
            TotalItems = result.TotalItems,
            Items = result.Items.Select(x => x.ToDomainModel().ToContract()).ToList(),
        };
    }
}
