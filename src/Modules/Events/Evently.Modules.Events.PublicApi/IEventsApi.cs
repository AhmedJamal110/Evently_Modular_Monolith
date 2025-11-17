namespace Evently.Modules.Events.PublicApi;
public interface IEventsApi
{
   Task<TicketTypeResponse?> GetTicketTypeAsync(
        Guid ticketTypeId,
        CancellationToken cancellationToken);
}

public sealed record TicketTypeResponse(
    Guid Id,
    Guid EventId,
    string Name,
    string Description,
    decimal Price,
    decimal Quantity);

