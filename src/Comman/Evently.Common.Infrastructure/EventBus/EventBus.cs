using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.EventBus;
using MassTransit;

namespace Evently.Common.Infrastructure.EventBus;
internal sealed class EventBus(IBus _bus ) : IEventBus
{
    public async Task PublishAsync<T>(
        T @event,
        CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
       await _bus.Publish(@event , cancellationToken);
    }
}
