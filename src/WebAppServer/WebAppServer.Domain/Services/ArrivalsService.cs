using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.Interfaces;
using WebAppServer.V1.Contracts;
using WebAppServer.Domain.Mappers;

namespace WebAppServer.Domain.Services;

public class ArrivalsService : IArrivalsService
{
    private readonly IArrivalRepository _arrivalRepository;

    public ArrivalsService(IArrivalRepository arrivalRepository)
    {
        _arrivalRepository = arrivalRepository;
    }

    public async Task<Page<ArrivalResponseContract>> GetArrivalsAsync(ArrivalRequestContract requestContract)
    {
        var requestDomain = requestContract.ToDomainModel();

        var result = await _arrivalRepository.GetAllAsync(
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
