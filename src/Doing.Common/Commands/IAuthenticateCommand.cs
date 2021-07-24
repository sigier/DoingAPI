using System;

namespace Doing.Common.Commands
{
    public interface IAuthenticateCommand: ICommand
    {
        Guid UserId {get; set;}
    }
}