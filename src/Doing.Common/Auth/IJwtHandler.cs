using System;


namespace Doing.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}