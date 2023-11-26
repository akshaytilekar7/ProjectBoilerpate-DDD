using NServiceBus;

namespace Sample.MediatR.Message.SagaData
{
    public class OrderSagaData : ContainSagaData
    {
        public int OrderId { get; set; }
        public bool IsPaymentProcessed { get; set; }
        public bool IsShipmentPrepared { get; set; }
    }
}
