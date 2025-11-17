using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Abstractions;
using Evently.Modules.Ticketing.Domain.Customers;

namespace Evently.Modules.Ticketing.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository _customerRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(
        CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            request.CustomerId,
            request.Email,
            request.FirstName,
            request.LastName);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
