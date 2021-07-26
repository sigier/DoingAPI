using System.Collections;
using System;
using System.Threading.Tasks;
using Doing.API.DTO;
using System.Collections.Generic;

namespace Doing.API.Repositories
{
    public interface IDoingRepository
    {
        Task<DoingDto> GetAsync(Guid id);

        Task AddAsync(DoingDto model);

        Task <IEnumerable<DoingDto>> BrowseAll(Guid userId);
    }
}