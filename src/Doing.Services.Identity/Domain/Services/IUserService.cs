using System.Threading.Tasks;
using Doing.Common.Auth;

namespace Doing.Services.Identity.Domain.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        
        Task<JsonWebToken> LogInAsync(string email, string password);
    }
}