using Dapperer;
using Microsoft.Extensions.Configuration;

namespace WebAppServer.Repository;

public class DappererSettings : IDappererSettings
{
    private readonly IConfiguration _configuration;

    public DappererSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ConnectionString => _configuration.GetConnectionString("DefaultConnection");
}
