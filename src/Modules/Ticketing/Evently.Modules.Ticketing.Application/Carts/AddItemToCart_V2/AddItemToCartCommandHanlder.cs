
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.PublicApi;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart_V2;

internal sealed class AddItemToCartCommandHanlder(
    ICustomerRepository _customerRepository,
    IEventsApi _eventsApi,
    CartService _cartService)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(
        AddItemToCartCommand request, 
        CancellationToken cancellationToken)
    {
        Customer? customer =
            await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
    
        if( customer is null)
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

        await _cartService.AddItemAsync(customer.Id, cartItem, cancellationToken);

        return Result.Success();



    }
}
