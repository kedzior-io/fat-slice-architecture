using MinimalCqrs;

namespace RichHandlerArchitecture.Infrastructure.Persistence;

public sealed class DomainEvent<IDomainEvent> : IEvent
{
    public IDomainEvent Payload { get; }

    public DomainEvent(IDomainEvent domainEvent)
    {
        Payload = domainEvent;
    }
}