using NServiceBus;

namespace SagaPattern.Saga.Messages
{
    public class StartShipment : ICommand
    {
        public int OrderId { get; set; }
    }

  
    public class CompleteShipment : ICommand
    {
        public int OrderId { get; set; }
    }
}
