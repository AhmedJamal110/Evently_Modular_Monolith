namespace Evently.Common.Application.EventBus;
public interface IIntegrationEvent
{
    Guid Id { get; set; }
     DateTime OccurredOnUTC { get; set; }

}
