using System;
using System.Threading;
using System.Threading.Tasks;

namespace Evently.Modules.Users.PublicAPI;

public interface IUsersApi
{ 
    Task<CustomerResponse?> GetAsync(
        Guid userId,
        CancellationToken cancellationToken);
}


public sealed record CustomerResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
