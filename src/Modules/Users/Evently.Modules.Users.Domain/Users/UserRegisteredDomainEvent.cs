using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid UserId) : DomainEvent
{
    public Guid UserId { get; } = UserId;
}
