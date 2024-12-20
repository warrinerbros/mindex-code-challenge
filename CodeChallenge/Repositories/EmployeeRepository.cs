using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public OneOf<Employee, NotFound, ServerError> GetEmployeeWithAllReportsById(string id)
        {
            Employee employee;
            try
            {
                employee = _employeeContext.Employees
                    .Include(e => e.DirectReports)
                    .ThenInclude(dr => dr.DirectReports)
                    .SingleOrDefault(e => e.EmployeeId == id);
            }
            catch (Exception e)
            {
                const string errorMessage = "An error occurred while getting employee and reports";
                _logger.LogError(e, errorMessage);
                return new ServerError(errorMessage);
            }

            if (employee == null)
            {
                _logger.LogDebug("Employee not found");
                return new NotFound();
            }

            _logger.LogDebug("Successfully fetched employee");
            return employee;
        }


        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
