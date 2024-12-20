using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;
using OneOf.Types;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Task<OneOf<Compensation, NotFound, ServerError>> GetById(String id);
        Task<OneOf<Success, BadRequest, ServerError>> Create(Compensation employee);
    }
}