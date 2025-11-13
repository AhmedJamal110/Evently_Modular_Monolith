namespace Evently.Modules.Ticketing.Domain.Orders;

public sealed record OrderDto(
    Guid Id,
    Guid CustomerId,
    string Status,
    decimal TotalPrice,
    string Currency,
    bool TicketsIssued,
    DateTime CreatedAtUtc);
