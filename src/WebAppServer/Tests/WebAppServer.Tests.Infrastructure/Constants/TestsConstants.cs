namespace WebAppServer.Tests.Infrastructure.Constants;

public class TestsConstants
{
    public class ErrorCodes
    {
        public const string ValidationError = nameof(ValidationError);
    }

    public class ErrorMessages
    {
        public class RequestValidation
        {
            public const string IntegerMustBePositive = "{0} must have a positive integer value.";
            public const string DateProvidedMustBeToday = "{0} must be a valid datetime today.";
        }

        public class BusinessValidation
        {
            public const string EntityWithIdNotFoundInDatabase = "Entity with such id was not found in the database.";
        }
    }
}
