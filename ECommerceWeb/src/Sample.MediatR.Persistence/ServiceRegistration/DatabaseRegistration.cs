using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Persistence.Configuration;
using Sample.MediatR.Persistence.Context;
using Sample.MediatR.Persistence.Middlewares;

namespace Sample.MediatR.Persistence.ServiceRegistration;

public static class DatabaseRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddMemoryCache();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(INServiceBus), typeof(NServiceBusService));
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();


        // Register the behaviors in the order you want them to be executed
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));


        services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(DatabaseRegistration).Assembly.FullName);
            });
        });

        return services;
    }
}
