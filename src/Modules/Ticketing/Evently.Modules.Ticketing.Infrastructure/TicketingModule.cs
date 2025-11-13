using Evently.Modules.Ticketing.Application.Abstractions;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services, IConfiguration configuration)
    {

        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing)));

        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<IEventRepository, EventRepository>();
        //services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        //services.AddScoped<IOrderRepository, OrderRepository>();
        //services.AddScoped<ITicketRepository, TicketRepository>();
        //services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

    }


}
