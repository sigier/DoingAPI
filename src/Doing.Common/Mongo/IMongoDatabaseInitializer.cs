using System.Threading.Tasks;

namespace Doing.Common.Mongo
{
    public interface IMongoDatabaseInitializer
    {
        Task InitializeAsync();
    }
}