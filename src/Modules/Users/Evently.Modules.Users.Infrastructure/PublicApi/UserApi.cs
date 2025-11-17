
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.PublicAPI;
using MediatR;

namespace Evently.Modules.Users.Infrastructure.PublicApi;
internal sealed class UserApi(
    ISender _sender) : IUsersApi
{
    public async Task<CustomerResponse?> GetAsync(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        Common.Domain.Result<UserResponse> result 
            = await _sender.Send(new GetUserQuery(userId), cancellationToken);
        
        if(result.IsFailure)
        {
            return null;
        }   

        return new CustomerResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName);
    }
}
