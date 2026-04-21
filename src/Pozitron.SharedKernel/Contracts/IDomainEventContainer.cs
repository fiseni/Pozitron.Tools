namespace Pozitron.SharedKernel;

public interface IDomainEventContainer
{
    IReadOnlyList<DomainEvent> Events { get; }
    void RegisterEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}
