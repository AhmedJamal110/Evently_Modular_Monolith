using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
using Evently.Modules.Events.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Infrastructure.PublicApi;
public sealed class EventsApi(ISender _sender) : IEventsApi
{
    public async Task<TicketTypeResponse?> GetTicketTypeAsync(
        Guid ticketTypeId,
        CancellationToken cancellationToken)
    {

        Common.Domain.Result<Domain.TicketTypes.TicketTypeDto?> result 
            = await _sender.Send(new GetTicketTypeQuery(ticketTypeId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new TicketTypeResponse(
            result.Value!.Id,
            result.Value.EventId,
            result.Value.Name,
            result.Value.Description,
            result.Value.Price,
            result.Value.Quantity);

    }
}
