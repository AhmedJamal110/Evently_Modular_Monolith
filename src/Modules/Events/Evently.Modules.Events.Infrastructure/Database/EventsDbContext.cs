using Microsoft.EntityFrameworkCore;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) 
    : DbContext(options) , IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
        modelBuilder.ApplyDeciamalConfiguration();
        modelBuilder.ApplyRestrictRelationConfigration();

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }
}
