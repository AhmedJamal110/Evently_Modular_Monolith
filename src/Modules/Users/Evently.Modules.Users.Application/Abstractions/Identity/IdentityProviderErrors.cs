using Evently.Common.Domain;

namespace Evently.Modules.Users.Application.Abstractions.Identity;

public static class  IdentityProviderErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        code: "IdentityProvider.EmailNotUnique",
        description: "The provided email is already in use.");
}
