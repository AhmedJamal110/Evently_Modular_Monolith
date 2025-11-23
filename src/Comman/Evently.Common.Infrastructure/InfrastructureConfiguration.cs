using Evently.Common.Application.Caching;
using Evently.Common.Application.Clock;
using Evently.Common.Application.EventBus;
using Evently.Common.Infrastructure.Authentication;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.EventBus;
using Evently.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string redisConnectionString)
    {

        services.AddAuthenticationInternal();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.AddScoped<IDateTimeProvider , DateTimeProvider>();

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });



        }
        catch
        {
            services.AddDistributedMemoryCache();
        }


        services.TryAddSingleton<ICacheService , CacheService>();
        services.TryAddSingleton<IEventBus ,EventBus.EventBus>();

        services.AddMassTransit(configur =>
        {
            foreach (var item in moduleConfigureConsumers)
            {
                item(configur);
            }

            configur.SetKebabCaseEndpointNameFormatter();

            configur.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });


        return services;
    }
}
