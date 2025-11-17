using Evently.Modules.Ticketing.Domain.Customers;

namespace Evently.Modules.Ticketing.Application.Carts;
public sealed class Cart
{
    public Guid CustomerId { get; set; }

    public List<CartItem> Items { get; init; } = [];

    public Customer Customer  { get; set; }

    
    internal static Cart CreateDefault(Guid customerId) =>
        new()
        {
            CustomerId = customerId
        };

}
