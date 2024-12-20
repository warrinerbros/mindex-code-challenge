using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService
        (
            ILogger<ReportingStructureService> logger,
            IEmployeeRepository employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<OneOf<ReportingStructure, NotFound, ServerError>> GetByEmployeeId(string employeeId)
        {
            var getReportsResult = await _employeeRepository.GetEmployeeWithAllReportsById(employeeId);
            if (!getReportsResult.TryPickT0(out var employee, out var getReportsFailure))
            {
                return getReportsFailure.Match<OneOf<ReportingStructure, NotFound, ServerError>>(
                    notFound => notFound,
                    serverError => serverError
                );
            }
            
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = GetNumberOfReports(employee)
            };
        }
        
        // Recursive function to get the number of reports for an employee
        private static int GetNumberOfReports(Employee employee)
        {
            // Base case: No direct reports
            if (employee.DirectReports == null || !employee.DirectReports.Any())
            {
                return 0;
            }

            var numberOfReports = 0;

            foreach (var report in employee.DirectReports)
            {
                numberOfReports++; // Count this report
                numberOfReports += GetNumberOfReports(report); // Recursively count the total reports this report has
            }

            return numberOfReports;
        }
    }
}