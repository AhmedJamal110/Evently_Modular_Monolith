using Evently.Common.Application.Clock;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.CancelEvent;
internal sealed class CancelEventCommandHandler(
    IEventRepository _eventRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICommandHandler<CancelEventCommand>
{
    public async Task<Result> Handle(
        CancelEventCommand request, 
        CancellationToken cancellationToken)
    {

        Event? @event = await _eventRepository.GetAsync(request.EventId, cancellationToken);
        
        if(@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        Result result = @event.Cancel(_dateTimeProvider.UTCNow);

        if(result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
