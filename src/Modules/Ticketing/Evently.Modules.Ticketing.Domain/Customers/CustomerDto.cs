namespace Evently.Modules.Ticketing.Domain.Customers;

public sealed record CustomerDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string IdentityId);
