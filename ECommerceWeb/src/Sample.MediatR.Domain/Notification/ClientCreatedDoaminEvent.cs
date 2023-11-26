using Sample.MediatR.Domain.Notification;

namespace Sample.MediatR.Persistence.Notification;
public class ClientCreatedDoaminEvent : BaseDomainEvent
{
    public int ClientId { get; private set; }

    public ClientCreatedDoaminEvent(int clientId)
    {
        ClientId = clientId;
    }
}
