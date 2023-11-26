using MediatR;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using SagaPattern.Saga.Messages;
using Sample.MediatR.Application.Order.Get;
using Sample.MediatR.Domain;
using Sample.MediatR.Application.Order.Create;

namespace Sample.MediatR.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageSession _messageSession;

        public OrderController(IMediator mediator, IMessageSession messageSession)
        {
            this._mediator = mediator;
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            var z = new GetOrderQuery();
            var list = await _mediator.Send(z);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromQuery] int clientId, [FromQuery] int productId)
        {
            var createOrderCommand = new CreateOrderCommand() { ClientId = clientId, ProductId = productId };

            var order = await _mediator.Send(createOrderCommand);
            var startOrderMessage = new StartOrder { OrderId = order.Id, };

            StartPayment x1 = new StartPayment() { OrderId = order.Id };
            StartShipment x2 = new StartShipment() { OrderId = order.Id };

            await _messageSession.SendLocal(startOrderMessage);

            Thread.Sleep(5000);
            //await _messageSession.Send("Application.Shipping", x2);

            return Ok(order);
        }
    }
}
