using System.Threading.Tasks;

namespace Doing.Services.Identity.Domain.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        
        Task LogInAsync(string email, string password);
    }
}