using Evently.Common.Application.Caching;
using Evently.Common.Application.Clock;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Clock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string redisConnectionString)
    {

        services.AddScoped<IDateTimeProvider , DateTimeProvider>();

        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.TryAddSingleton(connectionMultiplexer);

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
        });


        services.TryAddSingleton<ICacheService , CacheService>();


        return services;
    }
}
