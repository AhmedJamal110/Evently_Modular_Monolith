namespace Evently.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<UserDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);
}
