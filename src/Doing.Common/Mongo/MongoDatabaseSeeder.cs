using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Doing.Common.Mongo
{
    public class MongoDatabaseSeeder : IMongoDatabaseSeeder
    {
        private readonly IMongoDatabase _database;

        public MongoDatabaseSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public  async Task SeedAsync()
        {

            var collectionPointer =  await _database.ListCollectionsAsync();

            var collections = await collectionPointer.ToListAsync();

            if (collections.Any())
            {
                return;
            }

            await CustomSeedAsync();
        }

        protected virtual async Task  CustomSeedAsync()
        {
            await Task.CompletedTask;

        }
    }
}