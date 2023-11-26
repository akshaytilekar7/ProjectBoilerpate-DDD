using NServiceBus;
using SagaPattern.Saga.Messages;

namespace Payment.Handlers.Command
{
    public class StartPaymentHandler : IHandleMessages<StartPayment>
    {
        public Task Handle(StartPayment message, IMessageHandlerContext context)
        {
            Console.WriteLine(DateTime.Now+ $" CMD, \t StartPayment : {message} ");
            Thread.Sleep(19000);
            Console.WriteLine(DateTime.Now+" payment is in progress... wait...");
            Thread.Sleep(25000);
            Console.WriteLine(DateTime.Now + " payment is completed...");
            Thread.Sleep(6000);
            return context.Send(new CompletePayment() { OrderId = message.OrderId });
        }
    }
}
