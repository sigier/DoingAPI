using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Doing.Common.Services;
using Doing.Common.Commands;

namespace Doing.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUser>()
                .Build()
                .Run();
        }

        
    }
}
