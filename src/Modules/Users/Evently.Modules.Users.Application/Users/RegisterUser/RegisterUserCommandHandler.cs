using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.PublicApi;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService _identityProviderService
    IUserRepository userRepository,
    //ITicketingApi _ticketingApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userModel = new UserModel(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);

        Result<string> result = await _identityProviderService.RegisterUserAsync(userModel, cancellationToken);

        if(result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }


        var user = User.Create
            (request.Email,
            request.FirstName,
            request.LastName,
            result.Value);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //await _ticketingApi.CreateCustomerAsync(
        //    user.Id,
        //    user.Email,
        //    user.FirstName,
        //    user.LastName,
        //    cancellationToken);


        return user.Id;
    }
}
