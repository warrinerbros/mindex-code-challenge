using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _employeeService;

        public EmployeeController(ILogger<ReportingStructureController> logger, IReportingStructureService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{employeeId}", Name = "getEmployeeById")]
        public IActionResult GetReportingStructureByEmployeeId(String employeeId)
        {
            _logger.LogDebug("Received reporting structure get request for employeeId:\'{EmployeeId}\'", employeeId);

            var employee = _employeeService.GetById(employeeId);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
    }
}