using MediatR;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Message;
using Sample.MediatR.Persistence.Configuration;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.Persistence.NotificationHandler;

public class ClientCreatedNotificationEventHandler : INotificationHandler<ClientCreatedDoaminEvent>
{
    private readonly IRepository<Domain.Client> _repository;
    private readonly INServiceBus _messageSession;

    public ClientCreatedNotificationEventHandler(IRepository<Domain.Client> repository, INServiceBus messageSession = null)
    {
        _repository = repository;
        _messageSession = messageSession;
    }

    public async Task Handle(ClientCreatedDoaminEvent notification, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(notification.ClientId);
        var clientCreated = new CreateClient() { Client = client };
        await _messageSession.Send(clientCreated);
    }
}
