using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Api.Extensions;

public static class DatabaseExtension
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using IServiceScope serviceScope = app.Services.CreateScope();

        await using EventsDbContext eventsDbContext = serviceScope
             .ServiceProvider
             .GetRequiredService<EventsDbContext>();


        await using UsersDbContext usersDbContext = serviceScope
             .ServiceProvider
             .GetRequiredService<UsersDbContext>();


        try
        {
            //await eventsDbContext.Database.MigrateAsync();
            //await usersDbContext.Database.MigrateAsync();

            app.Logger
                .LogInformation("Database migrations applied successfully.");


        }
        catch (Exception ex)
        {
            app.Logger
                .LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }
}
