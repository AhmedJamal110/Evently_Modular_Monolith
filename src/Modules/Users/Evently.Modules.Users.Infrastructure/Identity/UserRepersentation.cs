namespace Evently.Modules.Users.Infrastructure.Identity;

internal sealed record UserRepersentation(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] CredentialRepresentations);
