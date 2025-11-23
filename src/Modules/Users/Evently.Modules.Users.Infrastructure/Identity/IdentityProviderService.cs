using System.Net;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityProviderService(
    KeyCloakClient _keyCloakClient,
    ILogger<IdentityProviderService> _logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "password";

    public async Task<Result<string>> RegisterUserAsync(
        UserModel userModel, 
        CancellationToken cancellationToken)
    {
        var userRepersentation = new UserRepersentation(
            userModel.Email,
            userModel.Email,
            userModel.FirstName,
            userModel.LastName,
            EmailVerified: true,
            Enabled: true,
            new[]
            {
                new CredentialRepresentation(
                    Type: PasswordCredentialType,
                    Value: userModel.Password,
                    Temporary: false)
            });


        try
        {
            string IdentityId = 
                await _keyCloakClient.RegisterUserAsync(userRepersentation, cancellationToken);

            return IdentityId;
        }
        catch (HttpRequestException exception) when(exception.StatusCode ==  HttpStatusCode.Conflict)
        {
            _logger.LogError(exception , "User Registration failed. User with email {Email} already exists", userModel.Email);

            return Result.Failure<string>(IdentityProviderErrors.EmailNotUnique);
        }



    }
}
