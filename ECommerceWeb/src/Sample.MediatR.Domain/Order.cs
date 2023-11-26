namespace Sample.MediatR.Domain;
public class Order : BaseEntity
{
    public Client Client { get; set; }

    public Product Product { get; set; }

    public OrderStatus Status { get; set; }


}
