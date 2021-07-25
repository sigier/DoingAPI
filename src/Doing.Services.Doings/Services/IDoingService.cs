using System;
using System.Threading.Tasks;

namespace Doing.Services.Doings.Services
{
    public interface IDoingService
    {
        Task AddAsync(Guid id, Guid userId, string category, 
            string name, string description, DateTime createdAt);
    }
}