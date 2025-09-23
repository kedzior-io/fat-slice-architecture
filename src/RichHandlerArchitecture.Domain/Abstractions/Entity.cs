using RichHandlerArchitecture.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RichHandlerArchitecture.Domain.Abstractions
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedAtUtc { get; protected set; }
        public EntityStatus EntityStatus { get; protected set; } = EntityStatus.Active;

        private readonly List<IDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
    }
}