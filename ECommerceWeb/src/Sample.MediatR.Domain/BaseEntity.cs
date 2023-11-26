using Sample.MediatR.Domain.Notification;

namespace Sample.MediatR.Domain
{
    public class BaseEntity
    {
        private readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();

        public int Id { get; set; }

        public bool HasQueuedEvents() => _events.Any();

        public void QueueEvent(BaseDomainEvent domainEvent) => _events.Add(domainEvent);

        public List<BaseDomainEvent> DequeueEvents()
        {
            var queuedEvents = new List<BaseDomainEvent>(_events);
            _events.Clear();
            return queuedEvents;
        }
    }
}
