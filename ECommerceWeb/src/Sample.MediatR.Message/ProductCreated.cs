using NServiceBus;
using Sample.MediatR.Domain;

namespace Sample.MediatR.Message;

public class ProductCreated : IEvent
{
    public Product Product { get; set; }

    public override string ToString()
    {
        return Product.Id + " " + Product.Name + " " + Product.Description + " " + Product.Quantity;
    }
}
