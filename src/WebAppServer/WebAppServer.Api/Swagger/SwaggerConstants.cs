namespace WebAppServer.Api.Swagger;

public class SwaggerConstants
{
    public class Arrivals
    {
        public class GetAll
        {
            public const string Summary = "Get All Arrivals";
            public const string Description = "The [GET] endpoint provides information on when employees arrived at work for a certain date, ordered and paged.";
        }
    }

    public class Reports
    {
        public class CreateReports
        {
            public const string Summary = "Create Arrival Reports";
            public const string Description = "The [POST] endpoint receives batches of arrival reports from WebServce and processes them in order for those to be stored in the WebAppServerDatabase.";
        }
    }
}
