namespace WebAppServer.Common.Constants;

public class ValidatorConstants
{
    public const string PropertyName = "{PropertyName}";

    public class ErrorCode
    {
        public const string ValidationError = nameof(ValidationError);
    }

    public class ErrorMessage
    {
        // Request validation
        public const string IntegerMustBePositive = "{PropertyName} must be a positive integer.";
        public const string IntegerMustBePositiveOrEmpty = "{PropertyName} must be either a positive integer or empty.";
        public const string IntegerMustBeNonNegativeOrEmpty = "{PropertyName} must be either a nonnegative integer or empty.";

        public const string DateProvidedMustBeToday = "{PropertyName} must be a valid datetime today.";
        public const string DateProvidedMustBeTodayOrInThePast = "{PropertyName} must be either today or in the past.";
        public const string DateFromProvidedMustBeBeforeDateTo = "'{0}' must be either before '{1}' or these should be equal.";

        public const string OrderMustBeAscDescOrEmpty = "{PropertyName} must be either 'ASC', 'DESC' or empty.";

        // Business rules validation
        public const string EntityWithIdNotFoundInDatabase = "Entity with such id was not found in the database.";
    }
}
