using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketType;
public sealed record GetTicketTypeQuery(Guid TicketTypeId) : IQuery<TicketTypeDto?>;

public sealed class GetTicketTypeQueryHandler(
    ITicketTypeRepository _ticketTypeRepository)
    : IQueryHandler<GetTicketTypeQuery, TicketTypeDto?>
{
    public async Task<Result<TicketTypeDto?>> Handle(
        GetTicketTypeQuery request,
        CancellationToken cancellationToken)
    {
        TicketTypeDto? ticketTypeDto =
            await _ticketTypeRepository.GetAsync(request.TicketTypeId, cancellationToken);

        if (ticketTypeDto is null)
        {
            return Result.Failure<TicketTypeDto?>(
                TicketTypeErrors.NotFound(request.TicketTypeId));   
        }

        return Result.Success<TicketTypeDto?>(ticketTypeDto);

    }
}
