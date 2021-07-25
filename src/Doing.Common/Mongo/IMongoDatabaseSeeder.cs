using System.Threading.Tasks;

namespace Doing.Common.Mongo
{
    public interface IMongoDatabaseSeeder
    {
        Task SeedAsync();
    }
}