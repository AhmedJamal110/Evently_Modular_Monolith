using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
public sealed record GetTicketTypesQuery(Guid EventId) 
    : IQuery<IReadOnlyCollection<TicketTypeDto?>>;
