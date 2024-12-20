using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug("Received compensation POST request for employeeId: \'{EmployeeEmployeeId}\'", compensation.Employee.EmployeeId);
            var createCompensationResult = await _compensationService.Create(compensation);
            return createCompensationResult.Match<IActionResult>(
                success => CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.Employee.EmployeeId }, compensation),
                badRequest => BadRequest(badRequest.Message),
                serverError => StatusCode(500, serverError.Message)
            );
        }

        [HttpGet("{employeeId}", Name = "getCompensationByEmployeeId")]
        public async Task<IActionResult> GetCompensationByEmployeeId(String employeeId)
        {
            _logger.LogDebug("Received compensation GET request for employeeId: \'{Id}\'", employeeId);
            var getCompensationResult = await _compensationService.GetById(employeeId);
            return getCompensationResult.Match<IActionResult>(
                compensation => Ok(compensation),
                notFound => NotFound(),
                serverError => StatusCode(500, serverError.Message)
            );
        }
    }
}