using MediatR;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using SagaPattern.Saga.Messages;
using Sample.MediatR.Application.Order.Get;
using Sample.MediatR.Domain;
using Sample.MediatR.Application.Order.Create;
using Sample.MediatR.Persistence.Configuration;

namespace Sample.MediatR.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INServiceBus _messageSession;

        public OrderController(IMediator mediator, INServiceBus messageSession)
        {
            this._mediator = mediator;
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var query = new GetOrderQuery() { Id = id };
            var client = await _mediator.Send(query);
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromQuery] int clientId, [FromQuery] int productId)
        {
            var createOrderCommand = new CreateOrderCommand() { ClientId = clientId, ProductId = productId };

            var order = await _mediator.Send(createOrderCommand);
            var startOrderMessage = new StartOrder { OrderId = order.Id, };

            await _messageSession.SendLocal(startOrderMessage);

            Thread.Sleep(5000);

            return Ok(order);
        }
    }
}
