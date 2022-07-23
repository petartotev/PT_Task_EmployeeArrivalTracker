namespace WebAppServer.Common.Constants;

public class ValidatorConstants
{
    public class ErrorCode
    {
        public const string ValidationError = nameof(ValidationError);
    }

    public class ErrorMessage
    {
        // Request validation
        public const string IntegerMustBePositive = "{PropertyName} must have a positive integer value.";
        public const string DateProvidedMustBeToday = "{PropertyName} must be a valid datetime today.";

        // Business rules validation
        public const string EntityWithIdNotFoundInDatabase = "Entity with such id was not found in the database.";
    }
}
