using System.Reflection;
using Dapperer;
using DbUp;
using Serilog;
using WebAppServer.Common.Constants;
using WebAppServer.Repository.DbUp.Interfaces;

namespace WebAppServer.Repository.DbUp;

public class DatabaseUpgrader : IDatabaseUpgrader
{
    private readonly IDappererSettings _settings;

    public DatabaseUpgrader(IDappererSettings settings)
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

        Log.Information(result.Scripts.Any()
            ? LoggerMessages.Database.Seeder.SeedSuccessful
            : LoggerMessages.Database.Seeder.DbUpToDate);

        return 0;
    }
}
