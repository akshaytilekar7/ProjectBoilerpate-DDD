using NServiceBus;
using Sample.MediatR.Message;

namespace Shipping.Handlers.Event
{
    public class ProductCreatedHandler : IHandleMessages<ProductCreated>
    {
        public Task Handle(ProductCreated productCreated, IMessageHandlerContext context)
        {
            Console.WriteLine(DateTime.Now + $" EVE, \t Product : {productCreated} ");
            return Task.CompletedTask;
        }
    }
}
