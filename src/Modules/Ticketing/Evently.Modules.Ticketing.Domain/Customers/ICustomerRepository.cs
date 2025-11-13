namespace Evently.Modules.Ticketing.Domain.Customers;

public interface ICustomerRepository
{
    Task<CustomerDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Customer customer);
}
