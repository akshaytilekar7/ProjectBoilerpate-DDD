using NServiceBus;
using SagaPattern.Saga.Messages;
using Sample.MediatR.Domain;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Message.SagaData;
using Sample.MediatR.Persistence.Context;

namespace Sample.MediatR.Persistence.NServiceBusHandler
{
    public class StartOrderSagaHandler : Saga<OrderSagaData>,
       IAmStartedByMessages<StartOrder>,
       IAmStartedByMessages<CompletePayment>,
       IAmStartedByMessages<CompleteShipment>
    {
        private readonly AppDbContext appDbContext;

        public StartOrderSagaHandler()
        {

        }
        public StartOrderSagaHandler(AppDbContext repository)
        {
            appDbContext = repository;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.MapSaga(saga => saga.OrderId)
                .ToMessage<StartOrder>(message => message.OrderId)
                .ToMessage<CompletePayment>(message => message.OrderId)
                .ToMessage<CompleteShipment>(message => message.OrderId);
        }

        public async Task Handle(StartOrder message, IMessageHandlerContext context)
        {
            Console.WriteLine(DateTime.Now + " start order");

            Data.OrderId = message.OrderId;

            if (appDbContext != null)
            {
                var order = appDbContext.Orders.Find(message.OrderId);
                order.Status = OrderStatus.Pending;
                appDbContext.Orders.Update(order);
            }

            StartPayment startPayment = new StartPayment() { OrderId = message.OrderId };
            await context.Send("Application.Payment", startPayment);
        }

        public async Task Handle(CompletePayment message, IMessageHandlerContext context)
        {
            Console.WriteLine(DateTime.Now + " start shipment");

            Data.IsPaymentProcessed = true;
            Order order = new Order() { Id = message.OrderId };
            if (appDbContext != null)
            {
                order = appDbContext.Orders.Find(message.OrderId);
                order.Status = OrderStatus.PaymentCompleted;
                appDbContext.Orders.Update(order);
                Console.WriteLine(DateTime.Now + " Now starting shipment process");
            }

            StartShipment startShipment = new StartShipment { OrderId = message.OrderId };
            await context.Send("Application.Shipping", startShipment);

            await OnCompleteBothAsync(context, order);

        }

        public async Task Handle(CompleteShipment message, IMessageHandlerContext context)
        {
            Data.IsShipmentPrepared = true;
            Thread.Sleep(3000);

            Order order = new Order() { Id = message.OrderId };
            if (appDbContext != null)
            {
                order = appDbContext.Orders.Find(message.OrderId);
                order.Status = OrderStatus.ShipmentCompleted;
                Thread.Sleep(1000);
                appDbContext.Orders.Update(order);
            }
            await OnCompleteBothAsync(context, order);
        }

        private async Task OnCompleteBothAsync(IMessageHandlerContext context, Order order)
        {
            if (Data.IsShipmentPrepared && Data.IsPaymentProcessed)
            {
                await context.Publish(new OrderCompleted { OrderId = order.Id });
                order.Status = OrderStatus.OrderCompleted;
                // appDbContext.Orders.Find(order);
                Console.WriteLine(DateTime.Now + " ALL PAYEMNT AND SHIPPING DONE");
                MarkAsComplete();
            }
            else if (Data.IsShipmentPrepared && !Data.IsPaymentProcessed)
                Console.WriteLine(DateTime.Now + " Payment is not done yet");
            else if (!Data.IsShipmentPrepared && Data.IsPaymentProcessed)
                Console.WriteLine(DateTime.Now + " Shipiing is not done yet");
        }

    }
}
