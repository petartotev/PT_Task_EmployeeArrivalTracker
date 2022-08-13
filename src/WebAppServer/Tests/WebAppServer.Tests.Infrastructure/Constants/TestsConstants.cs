namespace WebAppServer.Tests.Infrastructure.Constants;

public class TestsConstants
{
    public class ErrorCodes
    {
        public const string ValidationError = nameof(ValidationError);
        public const string UnauthorizedAccess = nameof(UnauthorizedAccess);
        public const string BadRequest = nameof(BadRequest);
    }

    public class ErrorMessages
    {
        public class RequestValidation
        {
            public const string FourthTokenIsARequiredHeader = "X-Fourth-Token is a required header.";
            public const string ProvidedTokenIsInvalid = "The provided token is invalid.";

            public const string IntegerMustBePositive = "{0} must be a positive integer.";
            public const string IntegerMustBePositiveOrEmpty = "{0} must be either a positive integer or empty.";
            public const string IntegerMustBeNonNegativeOrEmpty = "{0} must be either a nonnegative integer or empty.";

            public const string DateProvidedMustBeToday = "{0} must be a valid datetime today.";
            public const string DateProvidedMustBeTodayOrInThePast = "{0} must be either today or in the past.";
            public const string DateFromProvidedMustBeBeforeDateTo = "'{0}' must be either before '{1}' or these should be equal.";

            public const string OrderMustBeAscDescOrEmpty = "{0} must be either 'ASC', 'DESC' or empty.";
        }

        public class BusinessValidation
        {
            public const string EntityWithIdNotFoundInDatabase = "Entity with such id was not found in the database.";
        }
    }
}
