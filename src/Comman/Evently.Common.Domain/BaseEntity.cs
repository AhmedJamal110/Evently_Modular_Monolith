namespace Evently.Common.Domain;
public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];


    protected BaseEntity()
    {
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();


    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }



}
