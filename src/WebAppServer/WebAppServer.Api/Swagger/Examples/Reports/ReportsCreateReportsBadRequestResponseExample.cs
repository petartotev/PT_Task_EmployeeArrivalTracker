using Swashbuckle.AspNetCore.Filters;
using WebAppServer.Api.Extensions;
using WebAppServer.Common.Constants;
using WebAppServer.V1.Contracts;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Swagger.Examples.Reports;

public class ReportsCreateReportsBadRequestResponseExample : IExamplesProvider<Error[]>
{
    public Error[] GetExamples()
    {
        var result = new List<Error>
        {
            ErrorCode.ValidationError
                .AsError(ValidatorConstants.ErrorMessage.IntegerMustBePositive
                    .Replace(ValidatorConstants.PropertyName, nameof(ReportContract.EmployeeId))),
            ErrorCode.ValidationError
                .AsError(ValidatorConstants.ErrorMessage.DateProvidedMustBeToday
                    .Replace(ValidatorConstants.PropertyName, nameof(ReportContract.When))),
            ErrorCode.ValidationError
                .AsError(ValidatorConstants.ErrorMessage.EntityWithIdNotFoundInDatabase)
        };

        return result.ToArray();
    }
}
