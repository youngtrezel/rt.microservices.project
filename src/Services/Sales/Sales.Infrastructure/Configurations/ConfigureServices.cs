using EventBus.Constants;
using Sales.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sales.Infrastructure.Configurations;

    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PlateAddedEventConsumer>();
                x.AddConsumer<PlateReservedEventConsumer>();
                x.AddConsumer<PlateUnreservedEventConsumer>();
                x.AddConsumer<PlateSoldEventConsumer>();

                x.UsingRabbitMq((cxt, cfg) =>
                {
                    cfg.Host("host.docker.internal", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateAddedEventQueueName + "-sales", q =>
                    {
                        q.ConfigureConsumer<PlateAddedEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateReservedEventQueueName + "-sales", q =>
                    {
                        q.ConfigureConsumer<PlateReservedEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateUnreservedEventQueueName + "-sales", q =>
                    {
                        q.ConfigureConsumer<PlateUnreservedEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateSoldEventQueueName + "-sales", q =>
                    {
                        q.ConfigureConsumer<PlateSoldEventConsumer>(cxt);
                    });
                });
            });

            return services;
        }
    }

