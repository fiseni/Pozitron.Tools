using MediatR;

namespace Pozitron.SharedKernel;

public abstract class DomainEvent : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; } = Clock.Now;
}
