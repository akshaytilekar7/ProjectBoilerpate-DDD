using NServiceBus;

namespace SagaPattern.Saga.Messages
{
    public class OrderCompleted : IEvent
    {
        public int OrderId { get; set; }
    }
}
