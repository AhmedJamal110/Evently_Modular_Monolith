

namespace Evently.Modules.Events.Domain.Events;

public sealed record EventDto(
    Guid Id,
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    string Status);
