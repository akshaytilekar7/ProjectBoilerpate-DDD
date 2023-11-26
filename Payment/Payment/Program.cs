using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SagaPattern.Saga.Messages;

namespace Payment
{
    internal class Program
    {
        public const string TransportConnectionStringNormal = "host=host.docker.internal;port=5672;";
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var endpointInstance = ConfigureNServiceBus();
            services.AddSingleton<IMessageSession>(endpointInstance);

            Console.WriteLine("Enter to exist Payment");
            Console.ReadLine();
        }

        private static IEndpointInstance ConfigureNServiceBus()
        {
            var assemblyName = "Application.Payment";
            Console.Title = assemblyName;
            var endpointConfiguration = new EndpointConfiguration(assemblyName);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

            transport.ConnectionString(TransportConnectionStringNormal);
            transport.UseConventionalRoutingTopology();

            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(CompletePayment).Assembly, "Application.Client");

            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();

            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }
    }
}