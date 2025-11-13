namespace Evently.Modules.Users.Domain.Users;

public sealed record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string IdentityId);
