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

    public async Task<CategoryDto?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        CategoryDto? categoryDto = await _context.Categories
           .Where(x => x.Id == id)
           .Select(x => new CategoryDto(
                x.Id,
                x.Name,
                x.IsArchived))
           .FirstOrDefaultAsync(cancellationToken);
    
        if (categoryDto is null)
        {
            return null;
        }


        return categoryDto;

    }
}
