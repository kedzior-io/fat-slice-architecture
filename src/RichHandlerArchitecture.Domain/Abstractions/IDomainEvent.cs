namespace RichHandlerArchitecture.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime CreatedAtUtc { get; protected set; }
}