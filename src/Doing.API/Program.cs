using Doing.Common.Events;
using Doing.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Doing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                    .UseRabbitMq()
                    .SubscribeToEvent<CreateDoingEvent>()
                    .Build()
                    .Run();
        }

    }
}
