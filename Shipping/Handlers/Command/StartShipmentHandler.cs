using NServiceBus;
using SagaPattern.Saga.Messages;

namespace Shipping.Handlers.Command
{
    public class StartShipmentHandler : IHandleMessages<StartShipment>
    {
        public Task Handle(StartShipment message, IMessageHandlerContext context)
        {
            Console.WriteLine($"{DateTime.Now} CMD, \t StartShipment : {message} ");
            Thread.Sleep(5000);
            Console.WriteLine(DateTime.Now + " shipment is in progress... wait...");
            Thread.Sleep(8000);
            Console.WriteLine(DateTime.Now +" shipment is completed...");
            return context.Send(new CompleteShipment() { OrderId = message.OrderId });
        }
    }
}
