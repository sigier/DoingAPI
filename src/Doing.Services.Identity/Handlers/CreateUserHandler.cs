using System.Threading.Tasks;
using Doing.Common.Commands;

namespace Doing.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        public Task HandleAsync(CreateUser command)
        {
            throw new System.NotImplementedException();
        }
    }
}