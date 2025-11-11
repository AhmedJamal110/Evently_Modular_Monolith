using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.PublishEvent;

internal sealed class PublishEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PublishEventCommand>
{
    public async Task<Result> Handle(
        PublishEventCommand request, 
        CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetByIdAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        //if (!await ticketTypeRepository.ExistsAsync(@event.Id, cancellationToken))
        //{
        //    return Result.Failure(EventErrors.NoTicketsFound);
        //}

        @event.Publish();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
