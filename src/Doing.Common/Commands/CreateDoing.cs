using System;

namespace Doing.Common.Commands
{
    public class CreateDoing : IAuthenticateCommand
    {
        public Guid Id { get; set; }

        public Guid  UserId { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}