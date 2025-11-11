using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Categories;
internal sealed class CategoryRepository(
   EventsDbContext _context) : ICategoryRepository
{
    public void Add(Category category)
    {
        _context.Add(category);
    }

    public async Task<Category?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        Category? category = await _context.Categories
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync(cancellationToken);
    
        if (category is null)
        {
            return null;
        }


        return category;

    }
}
