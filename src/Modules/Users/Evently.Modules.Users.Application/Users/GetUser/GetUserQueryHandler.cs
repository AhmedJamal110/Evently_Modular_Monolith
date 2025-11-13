using System.Data.Common;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(
    IUserRepository _userRepository)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {

        User? user = await _userRepository.GetByIdAsync(request.UserId , cancellationToken);
       
        if(user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }


        var userResponse = new UserResponse(
            user.Id,
            user.Email,
            user.FirstName, 
            user.LastName);

        return userResponse;
    }
}
