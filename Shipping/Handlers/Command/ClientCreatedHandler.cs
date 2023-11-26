using NServiceBus;
using Sample.MediatR.Message;

namespace Shipping.Handlers.Command
{
    public class ClientCreatedHandler : IHandleMessages<CreateClient>
    {
        public Task Handle(CreateClient clientCreated, IMessageHandlerContext context)
        {
            Console.WriteLine(DateTime.Now + $" CMD, \t Client : {clientCreated} ");
            return Task.CompletedTask;
        }
    }
}
