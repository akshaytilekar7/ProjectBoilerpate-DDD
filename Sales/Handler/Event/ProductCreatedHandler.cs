using NServiceBus;
using Sample.MediatR.Message;

namespace Sales.Handlers
{
    public class ProductCreatedHandler : IHandleMessages<ProductCreated>
    {
        public Task Handle(ProductCreated productCreated, IMessageHandlerContext context)
        {
            Console.WriteLine($"EVE, \t Product : {productCreated} ");
            return Task.CompletedTask;
        }
    }
}
