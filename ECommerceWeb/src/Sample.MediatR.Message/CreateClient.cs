using NServiceBus;
using Sample.MediatR.Domain;

namespace Sample.MediatR.Message
{
    public class CreateClient : ICommand
    {
        public Client Client { get; set; }

        public override string ToString()
        {
            return Client.Id + " " + Client.Name + " " + Client.Email + " " + Client.Date;
        }
    }
}
