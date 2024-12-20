using CodeChallenge.Models;
using System;
using System.Threading.Tasks;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        Task<OneOf<ReportingStructure, NotFound, ServerError>> GetByEmployeeId(String id);
    }
}
