using System.Collections.Generic;
using System.Threading.Tasks;
using Doing.Services.Doings.Domain.Models;

namespace Doing.Services.Doings.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<CategoryModel> GetAsync(string name);

        Task<IEnumerable<CategoryModel>> BrowseAllAsync();

        Task AddAsync(CategoryModel category);
    }
}