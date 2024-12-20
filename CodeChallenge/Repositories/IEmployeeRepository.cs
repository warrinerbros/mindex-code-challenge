using CodeChallenge.Models;
using System;
using System.Threading.Tasks;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Task<OneOf<Employee, NotFound, ServerError>> GetEmployeeWithAllReportsById(string id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}