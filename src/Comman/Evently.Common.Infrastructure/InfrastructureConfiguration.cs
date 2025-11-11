using Evently.Common.Application.Clock;
using Evently.Common.Infrastructure.Clock;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {

        services.AddScoped<IDateTimeProvider , DateTimeProvider>();

        return services;
    }
}
