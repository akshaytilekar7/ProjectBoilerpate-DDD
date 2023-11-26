using Microsoft.Extensions.Configuration;
using NServiceBus;

namespace Sample.MediatR.Persistence.Configuration
{
    public class NServiceBusService : INServiceBus
    {
        private readonly IMessageSession messageSession;
        private readonly bool enable;

        public NServiceBusService(IMessageSession messageSession, IConfiguration configuration)
        {
            enable = Convert.ToBoolean(configuration["NServiceBusConfig:Enable"]);
            this.messageSession = messageSession;
        }
        public Task Publish(IEvent message)
        {
            if (!enable) return Log();
            return messageSession.Publish(message);
        }

        public Task Send(ICommand message)
        {
            if (!enable) return Log();
            return messageSession.Send(message);
        }

        public Task SendLocal(ICommand message)
        {
            if (!enable) return Log();
            return messageSession.SendLocal(message);
        }

        private Task Log()
        {
            Console.Write("NserviceBus is disable");
            return Task.CompletedTask;
        }
    }
}
