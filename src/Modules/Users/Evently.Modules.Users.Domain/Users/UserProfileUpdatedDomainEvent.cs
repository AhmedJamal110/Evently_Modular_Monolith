using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(Guid UserId, string FirstName, string LastName) : DomainEvent
{
    public Guid UserId { get; } = UserId;
    public string FirstName { get; } = FirstName;
    public string LastName { get; } = LastName;
}
