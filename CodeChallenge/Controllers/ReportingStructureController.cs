using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reporting-structure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{employeeId}", Name = "getReportingStructureByEmployeeId")]
        public async Task<IActionResult> GetReportingStructureByEmployeeId(String employeeId)
        {
            _logger.LogDebug("Received reporting structure GET request for employeeId:\'{EmployeeId}\'", employeeId);
            var reportingStructureResult = await _reportingStructureService.GetByEmployeeId(employeeId);
            return reportingStructureResult.Match<IActionResult>(
                reportingStructure => Ok(reportingStructure),
                notFound => NotFound(),
                serverError => StatusCode(500, serverError.Message)
            );
        }
    }
}
