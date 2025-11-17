namespace Evently.Modules.Events.Domain.TicketTypes;

public sealed record TicketTypeDto(
    Guid Id,
    Guid EventId,
    string Name,
    string Description,
    decimal Price,
    decimal Quantity);
