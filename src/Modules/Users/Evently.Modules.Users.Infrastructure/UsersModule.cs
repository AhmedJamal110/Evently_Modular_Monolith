using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Identity;
using Evently.Modules.Users.Infrastructure.PublicApi;
using Evently.Modules.Users.Infrastructure.Users;
using Evently.Modules.Users.PublicAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Evently.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
    

    private static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services.AddHttpClient<KeyCloakClient>((serviceProvider, httpclient) =>
        {
            KeyCloakOptions keyCloakOptions = serviceProvider
                .GetRequiredService<IOptions<KeyCloakOptions>>()
                .Value;

            httpclient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);


        }).AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();


        services.AddTransient<IIdentityProviderService , IdentityProviderService>();

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"),
                option => option
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUsersApi , UserApi>();
    }


}
