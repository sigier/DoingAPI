using System;
using System.Threading.Tasks;
using Doing.Services.Doings.Domain.Models;

namespace Doing.Services.Doings.Domain.Repositories
{
    public interface IDoingRepository
    { 
        Task<DoingModel> GetAsync(Guid id);

        Task AddAsync(DoingModel doing);
    }
    
}