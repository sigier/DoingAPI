using System;
using System.Reflection;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Doing.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
                IEventHandler<TEvent> handler) where TEvent: IEvent
        {
            return bus.SubscribeAsync<TEvent>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(
                        cfg => cfg.FromDeclaredQueue(queue => queue.WithName(GetQueueName<TEvent>()))
                )
            );
        }


        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
                ICommandHandler<TCommand> handler) where TCommand: ICommand
        {
          return bus.SubscribeAsync<TCommand>(
              msg => handler.HandleAsync(msg),
              ctx => ctx.UseSubscribeConfiguration(
                    cfg => cfg.FromDeclaredQueue(queue => queue.WithName(GetQueueName<TCommand>()))
              )
          );
        }


        private static string GetQueueName<T>()
        {
            return  $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }

        public static  IServiceCollection AddRabbitMq(this IServiceCollection services,
                    IConfiguration configuration)
        {
            var rabbitMqOptions = new RabbitMqOptions();

            var section = configuration.GetSection("rabbitmq");
            
            section.Bind(rabbitMqOptions);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions{
                    ClientConfiguration = rabbitMqOptions
            });

            services.AddSingleton<IBusClient>(_ => client);

            return services;
        }
    }
}