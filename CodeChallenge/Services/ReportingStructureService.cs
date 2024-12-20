using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger,
            IReportingStructureRepository reportingStructureRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee GetByEmployeeId(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                return _employeeRepository.GetById(employeeId);
            }

            return null;
        }
    }
}
