namespace WebAppServer.Common.Constants;

public class LoggerMessages
{
    public class Database
    {
        public const string EntityCreated = "Repository created a new {0} in database.";
        public const string FailedToCreateEntity = "Repository failed to create {0} in database.";

        public class Seeder
        {
            public const string SeedingRoles = "Seeding roles...";
            public const string SeedingTeams = "Seeding employing...";
            public const string SeedingEmployees = "Seeds employees. It can take a few minutes...";

            public const string SeedSuccessful = "Database upgraded successfully!";
            public const string DbUpToDate = "Database is up-to-date.";
        }
    }

    public class ExternalApi
    {
        public class WebService
        {
            public const string ServiceDown = $"{nameof(WebService)} appears to be down!";
            public const string ServiceUnavailable = $"{nameof(WebService)} is currently unavailable!";
            public const string SuccessfulSubscription = $"Application successfully subscribed to {nameof(WebService)}.";
        }
    }
}
