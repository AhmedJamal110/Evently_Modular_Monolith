using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories;

public sealed class CategoryCreatedDomainEvent(Guid caregoryId) : DomainEvent
{
    public Guid CaregoryId { get; } = caregoryId;
}
