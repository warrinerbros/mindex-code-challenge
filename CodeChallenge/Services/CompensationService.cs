using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(
            ILogger<CompensationService> logger, 
            ICompensationRepository compensationRepository,
            IEmployeeRepository EmployeeRepository
            )
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<OneOf<Success, BadRequest, ServerError>> Create(Compensation compensation)
        {
            var employee = _EmployeeRepository.GetById(compensation.Employee.EmployeeId);
            if (employee == null)
            {
                return new BadRequest("Employee not found. Make sure the employee exists before adding compensation.");
            }

            compensation.Employee = employee;
            var createCompensationResult = await _compensationRepository.Add(compensation);
            
            return createCompensationResult.Match<OneOf<Success, BadRequest, ServerError>>(
                success => new Success(),
                serverError => new ServerError("An error occurred while adding compensation")
            );
        }

        public async Task<OneOf<Compensation, NotFound, ServerError>> GetById(string id)
        {
            return await _compensationRepository.GetById(id);
        }
    }
}