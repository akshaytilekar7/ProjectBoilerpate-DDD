using NServiceBus;

namespace SagaPattern.Saga.Messages
{
    public class StartOrder : ICommand
    {
        public int OrderId { get; set; }
    }
}
