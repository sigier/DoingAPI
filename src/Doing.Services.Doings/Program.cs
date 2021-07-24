using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Doing.Services.Doings
{
    public class Program
    {   public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                    .UseRabbitMq()
                    .SubscribeToCommand<CreateDoing>()
                    .Build()
                    .Run();
        }
    }
}
