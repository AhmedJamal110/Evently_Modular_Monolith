namespace Evently.Modules.Ticketing.Domain.Payments;

public sealed record PaymentDto(
    Guid Id,
    Guid EventId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
