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

    public async Task<EventDto?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        EventDto? eventDto = await _context.Events
           .Where(x => x.Id == id)
           .Select(x => new EventDto(
               x.Id,
               x.CategoryId,
               x.Title,
               x.Description,
               x.Location,
               x.StartsAtUtc,
               x.EndsAtUtc,
               x.Status.ToString()))
           .FirstOrDefaultAsync(cancellationToken);

        if(eventDto is null)
        {
            return null;
        }

        return eventDto;

    }
}


