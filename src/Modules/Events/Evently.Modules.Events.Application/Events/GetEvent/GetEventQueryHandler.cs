using System.Data.Common;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(
    IEventRepository _eventRepository): IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(
        GetEventQuery request, 
        CancellationToken cancellationToken)
    {

        EventDto? eventDto = await _eventRepository.GetAsync(request.EventId, cancellationToken);
            
        if (eventDto is null)
        {
            return Result.Failure<EventResponse>(EventErrors.NotFound(request.EventId));
        }

        EventResponse response = new(
            eventDto.Id,
            eventDto.CategoryId,
            eventDto.Title,
            eventDto.Description,
            eventDto.Location,
            eventDto.StartsAtUtc,
            eventDto.EndsAtUtc);


        return response;
    }
}
