using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Events;
internal sealed class EventRepository(
    EventsDbContext _context) : IEventRepository
{
    public void Add(Event @event)
    {
        _context.Add(@event);
    }

    public async Task<Event?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        Event? @event = await _context.Events
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync(cancellationToken);

        if(@event is null)
        {
            return null;
        }

        return @event;

    }
}


