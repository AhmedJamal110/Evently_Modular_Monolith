namespace Evently.Common.Application.EventBus;

public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(Guid id , DateTime occurredOnUtc)
    {
        Id = id;
        OccurredOnUTC = occurredOnUtc;
    }

    public Guid Id { get ; set ; }
    public DateTime OccurredOnUTC { get; set; }
}
