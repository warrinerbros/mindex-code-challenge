using CodeChallenge.Models;
using System;
using System.Threading.Tasks;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Task<OneOf<Compensation, NotFound, ServerError>> GetById(String id);
        Task<OneOf<Success,  ServerError>> Add(Compensation employee);
    }
}