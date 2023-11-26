using NServiceBus;

namespace SagaPattern.Saga.Messages
{
    public class StartPayment : ICommand
    {
        public int OrderId { get; set; }
    }

    public class CompletePayment : ICommand
    {
        public int OrderId { get; set; }
    }
}
