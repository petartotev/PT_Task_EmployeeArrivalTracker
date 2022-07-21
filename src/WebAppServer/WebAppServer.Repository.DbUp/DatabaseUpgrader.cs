using DbUp;
using Serilog;
using System.Reflection;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Repository.DbUp.Interfaces;

namespace WebAppServer.Repository.DbUp;

public class DatabaseUpgrader : IDatabaseUpgrader
{
    private readonly IDbSettings _settings;

    public DatabaseUpgrader(IDbSettings settings)
    {
        _settings = settings;
    }

    public int Upgrade()
    {
        EnsureDatabase.For.SqlDatabase(_settings.ConnectionString);

        var result = DeployChanges.To
            .SqlDatabase(_settings.ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build()
            .PerformUpgrade();

        if (!result.Successful)
        {
            Log.Error(result.Error.Message);
#if DEBUG
            Console.ReadLine();
#endif
            return -1;
        }

        var areAnyScriptsExecuted = result.Scripts.Any();
        Log.Information(areAnyScriptsExecuted ? "Database upgraded successfully!" : "Database is up-to-date.");
        
        return 0;
    }
}
