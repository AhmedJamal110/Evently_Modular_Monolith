using Microsoft.EntityFrameworkCore;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Application.Abstractions.Data;

namespace Evently.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext : DbContext , IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
}
