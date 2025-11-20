using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.PublicApi;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(
    ISender _sender,
    //ITicketingApi _ticketingApi,
    IEventBus _eventBus) : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<UserResponse> result =
            await _sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery) ,result.Error);
        }

        // —Asynchrcncus_Ccmmunicataion
        //await _ticketingApi.CreateCustomerAsync(
        //result.Value.Id,
        //result.Value.Email,
        //result.Value.FirstName,
        //result.Value.LastName,
        //cancellationToken);


        // synchrcncus_Ccmmunicataion

        await _eventBus.PublishAsync(new UserRegisteredIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName), cancellationToken);

    }



}
