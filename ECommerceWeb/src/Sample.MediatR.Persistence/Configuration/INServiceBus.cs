using NServiceBus;

namespace Sample.MediatR.Persistence.Configuration
{
    public interface INServiceBus 
    {
        Task Send(ICommand message);

        Task Publish(IEvent message);

        Task SendLocal(ICommand message);

    }
}
