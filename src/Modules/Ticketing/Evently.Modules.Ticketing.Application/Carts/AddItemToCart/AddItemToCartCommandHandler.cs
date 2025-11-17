
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.PublicApi;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Users.PublicAPI;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart;

public sealed class AddItemToCartCommandHandler(
    IUsersApi _usersApi,
    IEventsApi  _eventsApi,
    CartService _cartService) : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(
        AddItemToCartCommand request, 
        CancellationToken cancellationToken)
    {
        CustomerResponse? customerResponse 
            = await _usersApi.GetAsync(request.CustomerId, cancellationToken);

        if (customerResponse is null)
        {
            return Result.Failure(
                CustomerErrors.NotFound(request.CustomerId));
        }

        TicketTypeResponse? ticketTypeResponse =
            await _eventsApi.GetTicketTypeAsync(request.TicketTypeId, cancellationToken);


        if (ticketTypeResponse is null)
        {
            return Result.Failure(
                TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        var cartItem = new CartItem()
        {
            TicketTypeId = ticketTypeResponse.Id,
            Quantity = ticketTypeResponse.Quantity,
            Price = request.Quantity,
            //Currency = ticketTypeResponse.Currency
        };

        await _cartService.AddItemAsync(customerResponse.Id, cartItem, cancellationToken);
        
        return Result.Success();

    }

}
