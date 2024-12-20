using System;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<CompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public async Task<OneOf<Success, ServerError>> Add(Compensation compensation)
        {
            try
            {
                await _employeeContext.Compensations.AddAsync(compensation);
                await _employeeContext.SaveChangesAsync();
                _logger.LogDebug("Successfully added compensation");
                return new Success();
            }
            catch (Exception e)
            {
                const string errorMessage = "An error occurred while adding compensation";
                _logger.LogError(e, errorMessage);
                return new ServerError(errorMessage);
            }
        }

        public async Task<OneOf<Compensation, NotFound, ServerError>> GetById(string id)
        {
            try
            {
                var compensation =  await _employeeContext.Compensations
                    .Include(c => c.Employee)
                    .SingleOrDefaultAsync(c => c.Employee.EmployeeId == id);

                if (compensation == null)
                {
                    _logger.LogDebug("Compensation not found for employeeId: \'{Id}\'", id);
                    return new NotFound();
                }

                _logger.LogDebug("Successfully retrieved compensation for employeeId: \'{Id}\'", id);
                return compensation;
            }
            catch (Exception e)
            {
                const string errorMessage = "An error occurred while getting compensation";
                _logger.LogError(e, errorMessage);
                return new ServerError(errorMessage);
            }
        }
    }
}
