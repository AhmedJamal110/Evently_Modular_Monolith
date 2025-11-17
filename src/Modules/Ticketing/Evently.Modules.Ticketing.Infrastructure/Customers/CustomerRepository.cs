using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Ticketing.Domain.Customers;

namespace Evently.Modules.Ticketing.Infrastructure.Customers;
internal sealed class CustomerRepository : ICustomerRepository
{
    public void Add(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
