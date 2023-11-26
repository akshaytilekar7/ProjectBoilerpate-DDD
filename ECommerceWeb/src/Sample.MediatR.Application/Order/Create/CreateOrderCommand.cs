using MediatR;

namespace Sample.MediatR.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<Domain.Order>
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }

    }
}
