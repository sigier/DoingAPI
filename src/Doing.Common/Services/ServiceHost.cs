using System.Windows.Input;
using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using Doing.Common.Commands;
using Doing.Common.Events;
using Doing.Common.RabbitMq;

namespace Doing.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webhost;

        public ServiceHost(IWebHost webhost)
        {
            _webhost = webhost;
        }

        public void Run()
        {
           _webhost.Run();
        }

        public static HostBuilder Create<TStartup>(string[] args) where TStartup: class 
        {
            Console.Title = typeof(TStartup).Namespace; 

            var configuration = new ConfigurationBuilder()
                        .AddEnvironmentVariables()
                        .AddCommandLine(args)
                        .Build();
            
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                        .UseConfiguration(configuration) 
                        .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                        .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }


        public class HostBuilder: BuilderBase
        {
            private IWebHost _webHost;

            private  IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
               return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
                private IWebHost _webHost;

                private  IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : Commands.ICommand
            {

                var handler =
                  (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
            
                _bus.WithCommandHandlerAsync(handler);

                return this;           
            }

             public BusBuilder SubscribeToEvent<TEvent>() where TEvent : Events.IEvent
            {
                var handler =
                  (IEventHandler<TEvent>)_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
            
                _bus.WithEventHandlerAsync(handler);

                return this;            
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}