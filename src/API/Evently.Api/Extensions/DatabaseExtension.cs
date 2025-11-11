using Evently.Modules.Events.Infrastructure.Database;
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

        try
        {
            await eventsDbContext.Database.MigrateAsync();
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
