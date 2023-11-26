using MediatR;

namespace Sample.MediatR.Domain.Notification;
public class CreateProductNotification : INotification
{
    public Product Product { get; set; }
}
