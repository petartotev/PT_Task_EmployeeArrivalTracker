using WebAppServer.V1.Contracts;

namespace WebAppServer.Domain.Services.Interfaces;

public interface IArrivalsService
{
    Task<Page<ArrivalContract>> GetArrivalsAsync(ArrivalRequestContract request);
}
