using Bede.Cashier.V2.Application.Common.Validation.Interfaces;
using FluentValidation;
using WebAppServer.Common;
using WebAppServer.Domain.Exceptions;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.Interfaces;
using WebAppServer.V1.Contracts;
using static WebAppServer.Common.Constants.ValidatorConstants;

namespace WebAppServer.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IRequestValidator _requestValidator;
    private readonly IValidator<ReportContract> _reportRequestValidator;
    private readonly IDbContext _dbContext;

    public ReportsService(
        IRequestValidator requestValidator,
        IValidator<ReportContract> reportRequestValidator,
        IDbContext dbContext)
    {
        _requestValidator = requestValidator;
        _reportRequestValidator = reportRequestValidator;
        _dbContext = dbContext;
    }

    public async Task CreateReportsAsync(IEnumerable<ReportContract> request)
    {
        foreach (var report in request)
        {
            _requestValidator.Validate(_reportRequestValidator, report);

            await ValidateEmployeeExistsInDatabaseAsync(report.EmployeeId);

            // TODO: Bulk insert instead of foreach!
            await _dbContext.ArrivalRepo.CreateAsync(report.EmployeeId, report.When);
        }
    }

    private async Task ValidateEmployeeExistsInDatabaseAsync(int employeeId)
    {
        var employeeInDb = await _dbContext.EmployeeRepo.GetSingleOrDefaultAsync(employeeId);

        if (employeeInDb == null)
        {
            throw new ValidatorAppException(new AppError(ErrorCode.ValidationError, ErrorMessage.EntityWithIdNotFoundInDatabase));
        }
    }
}
