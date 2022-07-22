using Dapperer;
using WebAppServer.Common.Configuration.Interfaces;

namespace WebAppServer.Repository;

public class DappererSettings : IDappererSettings
{
    private readonly IDbSettings _settings;

    public DappererSettings(IDbSettings settings)
    {
        _settings = settings;
    }

    public string ConnectionString => _settings.ConnectionString;
}
