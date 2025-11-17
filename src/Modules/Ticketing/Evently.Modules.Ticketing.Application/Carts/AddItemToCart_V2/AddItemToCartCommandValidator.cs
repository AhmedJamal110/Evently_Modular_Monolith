using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart_V2;

public sealed class AddItemToCartCommandValidator 
    : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.TicketTypeId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(decimal.Zero);
    }
}
