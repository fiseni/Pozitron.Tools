namespace Pozitron.SharedKernel;

public interface IDomainEventContainer
{
    public IEnumerable<DomainEvent> Events { get; }
    void RegisterEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}
