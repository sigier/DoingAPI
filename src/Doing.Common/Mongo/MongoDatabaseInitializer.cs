using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Doing.Common.Mongo
{
    public class MongoDatabaseInitializer : IMongoDatabaseInitializer
    {
        private bool _initialized;

        private readonly bool _seed;

        private readonly IMongoDatabase _database;
        private readonly IMongoDatabaseSeeder _seeder;

        public MongoDatabaseInitializer(IMongoDatabase database,
                IMongoDatabaseSeeder seeder,
                IOptions<MongoOptions> options)
        {
            _seeder = seeder;
            _database = database;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            RegisterConventions();

            _initialized = true;

            if (!_seed) return;

            await _seeder.SeedAsync();

        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("DoingConventions",
                new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions =>

                 new List<IConvention> {
                    new IgnoreExtraElementsConvention(true),
                    new EnumRepresentationConvention(BsonType.String),
                    new CamelCaseElementNameConvention()
                };

        }
    }
}