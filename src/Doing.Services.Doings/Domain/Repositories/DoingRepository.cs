using System;
using System.Threading.Tasks;
using Doing.Services.Doings.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Doing.Services.Doings.Domain.Repositories
{
    public class DoingRepository : IDoingRepository
    {

        private readonly IMongoDatabase _database;

        public DoingRepository(IMongoDatabase database)
        {
            _database = database;
        }

         private IMongoCollection<DoingModel> Collection =>
            _database.GetCollection<DoingModel>("Doings");


        public async Task AddAsync(DoingModel doing)
        {
            await Collection.InsertOneAsync(doing);
        }

        public async Task<DoingModel> GetAsync(Guid id)
        {
            return await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}