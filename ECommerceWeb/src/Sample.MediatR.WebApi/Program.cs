using Microsoft.EntityFrameworkCore;
using Sample.MediatR.Application;
using Sample.MediatR.Persistence.Extensions;
using MediatR;
using Sample.MediatR.Domain;
using Sample.MediatR.Persistence;
using Sample.MediatR.Persistence.ServiceRegistration;
using Sample.MediatR.Persistence.NServiceBusHandler;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // builder.AddSerilog("API MediatR", builder.Configuration);

    builder.Services.AddApiConfiguration();

    string connection = builder.Configuration.GetConnectionString("AppConnectionString");
    builder.Services.ConfigureNServiceBus();
    builder.Services.AddPersistence(connection);
    builder.Services.AddAutoMapper(typeof(MapperProfile));

    var array = new[] {
        typeof(Program).Assembly,
        typeof(BaseEntity).Assembly,
        typeof(MapperProfile).Assembly,
        typeof(Repository<>).Assembly,
        typeof(StartOrderSagaHandler).Assembly
    };

    builder.Services.AddMediatR(array);

    var app = builder.Build();
    app.UseApiConfiguration();

    await app.RunAsync();
}
catch (AggregateException ex)
{
    foreach (var innerException in ex.InnerExceptions)
    {
        Console.WriteLine(innerException.Message);
        // Log or inspect each inner exception to identify the root cause.
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.ToString());
    Console.WriteLine("Host terminated unexpectedly");
}
finally
{
    Console.WriteLine("Server Shutting down...");
}
