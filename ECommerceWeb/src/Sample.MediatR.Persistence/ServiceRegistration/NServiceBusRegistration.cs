using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SagaPattern.Saga.Messages;
using Sample.MediatR.Domain;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Message;
using Sample.MediatR.Persistence.Context;

namespace Sample.MediatR.Persistence.ServiceRegistration
{
    public static class NServiceBusRegistration
    {
        public const string TransportConnectionString = "host=host.docker.internal;port=5672;";

        public static void ConfigureNServiceBus(this IServiceCollection services)
        {
            var endpointInstance = ConfigureNServiceBus();
            services.AddSingleton<IMessageSession>(endpointInstance);
        }

        private static IEndpointInstance ConfigureNServiceBus()
        {
            var assemblyName = "Application.Client";

            var endpointConfiguration = new EndpointConfiguration(assemblyName);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(TransportConnectionString);

            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();

            //endpointConfiguration.RegisterComponents(components =>
            //{
            //    components.RegisterSingleton(typeof(IRepository<Order>), typeof(Repository<Order>));
            //});

            //endpointConfiguration.RegisterComponents(components =>
            //{
            //    components.RegisterSingleton(typeof(IRepository<Order>), typeof(Repository<Order>));
            //});

            //endpointConfiguration.RegisterComponents(
            //registration: configureComponents =>
            //{
            //    configureComponents.ConfigureComponent<AppDbContext>(DependencyLifecycle.SingleInstance);
            //});


            var routing = transport.Routing();

            // routing.RouteToEndpoint(typeof(CreateClient).Assembly, "Application.Shipping");

            routing.RouteToEndpoint(typeof(StartOrder).Assembly, "Application.Client");

            routing.RouteToEndpoint(typeof(StartShipment).Assembly, "Application.Shipping");
            routing.RouteToEndpoint(typeof(StartPayment).Assembly, "Application.Payment");



            var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            return endpointInstance;
        }
    }
}
