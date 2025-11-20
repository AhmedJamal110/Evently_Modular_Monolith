using Evently.Common.Application.EventBus;

namespace Evently.Modules.Users.IntegrationEvents;
public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string email,
        string firstName,
        string lastName) : base(id, occurredOnUtc)
    {
        Email = email;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; }
    public Guid UserId { get; }
    public string FirstName { get; }
    public string LastName { get; }
}
