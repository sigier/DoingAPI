using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doing.Common.Mongo;
using Doing.Services.Doings.Domain.Models;
using Doing.Services.Doings.Domain.Repositories;
using MongoDB.Driver;

namespace Doing.Services.Doings.Services
{
    public class CustomMongoSeeder : MongoDatabaseSeeder
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomMongoSeeder(IMongoDatabase database,
            ICategoryRepository categoryRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task  CustomSeedAsync()
        {
            var categoryList = new List<string> {
                "sport",
                "hobby",
                "work"
            };

            await Task.WhenAll(categoryList.Select( 
                c =>
                _categoryRepository.AddAsync(new CategoryModel(c))
            ));
        }
    }
}