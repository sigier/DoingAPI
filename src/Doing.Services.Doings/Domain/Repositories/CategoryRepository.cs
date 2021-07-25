using System.Collections.Generic;
using System.Threading.Tasks;
using Doing.Services.Doings.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Doing.Services.Doings.Domain.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _database;

       
        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

         private IMongoCollection<CategoryModel> Collection =>
            _database.GetCollection<CategoryModel>("Categories");

        public async Task<CategoryModel> GetAsync(string name)
        {
            return await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());
        }

        public async Task AddAsync(CategoryModel category)
        {
            await Collection.InsertOneAsync(category);
        }

        public async Task<IEnumerable<CategoryModel>> BrowseAllAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }

        
    }
}