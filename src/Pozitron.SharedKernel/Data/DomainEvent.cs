namespace Pozitron.SharedKernel;

public abstract class DomainEvent
{
    public DateTimeOffset DateOccurred { get; protected set; } = Clock.Now;
}
