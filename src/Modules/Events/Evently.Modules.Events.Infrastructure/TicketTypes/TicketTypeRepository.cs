using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Events.Infrastructure.TicketTypes;
public sealed class TicketTypeRepository(
    EventsDbContext _eventsDbContext) : ITicketTypeRepository
{
    public void Add(TicketType ticketType)
    {
        _eventsDbContext.Add(ticketType);
    }

    public async Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _eventsDbContext.TicketTypes
            .AnyAsync(t => t.EventId == eventId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TicketTypeDto?>> GetAllAsync(
        Guid EventId,
        CancellationToken cancellationToken = default)
    {
        List<TicketTypeDto> ticketTypeDtos = await _eventsDbContext.TicketTypes
            .Where(x => x.EventId == EventId)
            .Select(t => new TicketTypeDto(
                t.Id,
                t.EventId,
                t.Name,
                t.Name,
                t.Price,
                t.Quantity))
            .ToListAsync(cancellationToken: cancellationToken);
    
        if(ticketTypeDtos.Count == 0)
        {
            return Array.Empty<TicketTypeDto?>();
        }


        return ticketTypeDtos;
    }

    public async Task<TicketTypeDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TicketTypeDto? ticketTypeDto = await _eventsDbContext.TicketTypes
            .Where(x => x.Id == id)
            .Select(t => new TicketTypeDto(
                t.Id,
                t.EventId,
                t.Name,
                t.Name,
                t.Price,
                t.Quantity))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (ticketTypeDto == null)
        {
            return null;
        }


        return ticketTypeDto;
    }

    public Task<TicketType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _eventsDbContext.TicketTypes
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}

