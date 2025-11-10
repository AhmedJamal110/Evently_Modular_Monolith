

namespace Evently.Modules.Events.Domain.Events;
public interface IEventRepository
{
    Task<EventDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Event @event);
}
