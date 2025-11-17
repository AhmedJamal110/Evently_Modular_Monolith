using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;

public sealed class GetTicketTypesQueryHandler(
    ITicketTypeRepository _ticketTypeRepository)
    : IQueryHandler<GetTicketTypesQuery, IReadOnlyCollection<TicketTypeDto?>>
{
    public async Task<Result<IReadOnlyCollection<TicketTypeDto?>>> Handle(
        GetTicketTypesQuery request,
        CancellationToken cancellationToken)
    {

        IReadOnlyCollection<TicketTypeDto?> readOnlyCollection =
            await _ticketTypeRepository.GetAllAsync(request.EventId, cancellationToken);

        if(readOnlyCollection.Count == 0)
        {
            return Result.Failure<IReadOnlyCollection<TicketTypeDto?>>
                (TicketTypeErrors.NotFound(request.EventId));
        }

        return Result.Success(readOnlyCollection);

    }
}
