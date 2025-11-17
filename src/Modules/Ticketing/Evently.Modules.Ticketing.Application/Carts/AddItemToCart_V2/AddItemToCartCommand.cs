
using Evently.Common.Application.Messaging;
using Evently.Modules.Ticketing.Application.Abstractions;
using Evently.Modules.Users.PublicAPI;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart_V2;
public sealed record AddItemToCartCommand(
    Guid CustomerId,
    Guid TicketTypeId,
    decimal Quantity) : ICommand;
