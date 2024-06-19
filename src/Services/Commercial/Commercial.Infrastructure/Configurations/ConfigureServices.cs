using Commercial.Infrastructure.Consumers;
using EventBus.Constants;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Commercial.Infrastructure.Configurations
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {

                x.AddConsumer<PlateSoldEventConsumer>();

                x.UsingRabbitMq((cxt, cfg) =>
                {
                    cfg.Host("host.docker.internal", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint(QueuesConsts.PlateSoldEventQueueName + "-commercial", q =>
                    {
                        q.ConfigureConsumer<PlateSoldEventConsumer>(cxt);
                    });

                });
            });

            return services;
        }
    }
}
