using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

namespace Sales
{
    internal class Program
    {
        public const string TransportConnectionStringNormal = "host=host.docker.internal;port=5672;";
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var endpointInstance = ConfigureNServiceBus();
            services.AddSingleton<IMessageSession>(endpointInstance);

            Console.WriteLine("Enter to exist SALES");
            Console.ReadLine();
        }

        private static IEndpointInstance ConfigureNServiceBus()
        {
            var assemblyName = "Application.Sales";
            Console.Title = assemblyName;
            var endpointConfiguration = new EndpointConfiguration(assemblyName);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

            transport.ConnectionString(TransportConnectionStringNormal);
            transport.UseConventionalRoutingTopology();

            endpointConfiguration.EnableInstallers();

            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }
    }
}