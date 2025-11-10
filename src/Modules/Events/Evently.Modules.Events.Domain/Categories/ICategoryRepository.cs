namespace Evently.Modules.Events.Domain.Categories;
public interface ICategoryRepository
{
    Task<CategoryDto?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default);

    void Add(Category category);
}

public sealed record CategoryDto(
    Guid Id,
    string Name,
    bool IsArchive);
