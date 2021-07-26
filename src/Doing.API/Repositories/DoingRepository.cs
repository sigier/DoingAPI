using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doing.API.DTO;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Doing.API.Repositories
{
    public class DoingRepository : IDoingRepository
    {
         private readonly IMongoDatabase _database;

       
        public DoingRepository(IMongoDatabase database)
        {
            _database = database;
        }

         private IMongoCollection<DoingDto> Collection =>
            _database.GetCollection<DoingDto>("Doings");

        public async Task AddAsync(DoingDto model)
        {
            await Collection.InsertOneAsync(model);
        }

        public async Task<IEnumerable<DoingDto>> BrowseAllAsync(Guid userId)
        {
             return await Collection
                    .AsQueryable()
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
        }

        public async Task<DoingDto> GetAsync(Guid id)
        {
            return await Collection
                    .AsQueryable()
                    .FirstOrDefaultAsync(c => c.Id == id);
                    
        }
    }
}