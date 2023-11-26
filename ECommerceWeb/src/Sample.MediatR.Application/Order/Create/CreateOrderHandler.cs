using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.MediatR.Application.Client.Create;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Persistence.Notification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.MediatR.Application.Order.Create
{
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Domain.Order>
    {
        private readonly IRepository<Domain.Client> _repoClient;
        private readonly IRepository<Domain.Product> _repoProduct;
        private readonly IRepository<Domain.Order> _repoOrder;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateOrderHandler(IRepository<Domain.Order> repoOrder,
            IRepository<Domain.Client> repoClient,
            IRepository<Domain.Product> repoProduct, IMapper mapper, IMediator mediator)
        {
            _repoClient = repoClient;
            _repoProduct = repoProduct;
            _repoOrder = repoOrder;
            _mapper = mapper;
            this._mediator = mediator;
        }
        public async Task<Domain.Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var prod = await _repoProduct.GetByIdAsync(request.ProductId);
            var cli = await _repoClient.GetByIdAsync(request.ClientId);

            var order = new Domain.Order() { Client = cli, Product = prod, Status = Domain.OrderStatus.OrderCreated };
            order = await _repoOrder.AddAsync(order);
            return await Task.FromResult(order);
        }
    }
}
