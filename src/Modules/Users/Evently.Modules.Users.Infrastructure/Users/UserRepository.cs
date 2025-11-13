using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext _context) : IUserRepository
{
    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<UserDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(x => x.Id == id)
            .Select(x => new UserDto(
                x.Id, 
                x.Email,
                x.FirstName,
                x.LastName,
                x.IdentityId))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

  
   
}
