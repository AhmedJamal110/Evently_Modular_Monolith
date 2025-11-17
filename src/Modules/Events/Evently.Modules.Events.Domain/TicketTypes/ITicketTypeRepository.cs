namespace Evently.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository
{
    Task<IReadOnlyCollection<TicketTypeDto?>> GetAllAsync(
        Guid EventId,
        CancellationToken cancellationToken = default);

    Task<TicketTypeDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TicketType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default);

    void Add(TicketType ticketType);
}
