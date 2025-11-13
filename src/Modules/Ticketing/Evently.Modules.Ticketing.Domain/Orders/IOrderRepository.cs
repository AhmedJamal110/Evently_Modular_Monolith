namespace Evently.Modules.Ticketing.Domain.Orders;

public interface IOrderRepository
{
    Task<OrderDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Order order);
}
