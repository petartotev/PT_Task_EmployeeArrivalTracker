using WebAppServer.Common.Configuration.Interfaces;

namespace WebAppServer.Common.Configuration;

public class EnvironmentSettings : IDbSettings
{
    public string ConnectionString
    {
        get
        {
            return "Server=localhost;Database=EmployeeArrivalTracker;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }

    public string ConnectionUrlWebService
    {
        get
        {
            return $"http://localhost:51396/api/clients/subscribe?date={DateTime.Now.ToString("yyyy-MM-dd")}&callback=https://localhost:7168/api/reports";
        }
    }
}
