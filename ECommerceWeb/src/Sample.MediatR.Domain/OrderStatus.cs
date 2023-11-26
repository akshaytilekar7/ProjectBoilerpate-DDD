namespace Sample.MediatR.Domain
{
    public enum OrderStatus
    {
        OrderCreated = 10,
        Pending = 20,

        PaymentCompleted = 30,
        ShipmentCompleted= 40,

        OrderCompleted = 50
    }
}
