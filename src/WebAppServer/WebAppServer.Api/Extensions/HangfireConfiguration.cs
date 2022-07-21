using Hangfire;
using Hangfire.SqlServer;

namespace WebAppServer.Api.Extensions;

public static class HangfireConfiguration
{
    public static IServiceCollection UseHangfire(this IServiceCollection services, string connectionString)
    {
        return services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }))
            .AddHangfireServer();
    }
}
