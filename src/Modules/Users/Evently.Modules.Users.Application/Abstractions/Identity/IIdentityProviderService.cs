using Evently.Common.Domain;

namespace Evently.Modules.Users.Application.Abstractions.Identity;
public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(
        UserModel userModel, 
        CancellationToken cancellationToken);
}
