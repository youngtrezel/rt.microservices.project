using EventBus.Constants;
using Marketing.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketing.Infrastructure.Configurations;

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

                    cfg.ReceiveEndpoint(QueuesConsts.PlateSoldEventQueueName + "-marketing", q =>
                    {
                        q.ConfigureConsumer<PlateSoldEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateAddedEventQueueName + "-marketing", q =>
                    {
                        q.ConfigureConsumer<PlateAddedEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateReservedEventQueueName + "-marketing", q =>
                    {
                        q.ConfigureConsumer<PlateReservedEventConsumer>(cxt);
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateUnreservedEventQueueName + "-marketing", q =>
                    {
                        q.ConfigureConsumer<PlateUnreservedEventConsumer>(cxt);
                    });
                });
            });

            return services;
        }
    }

