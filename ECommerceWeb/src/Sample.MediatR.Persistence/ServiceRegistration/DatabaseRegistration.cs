using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.MediatR.Domain;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Persistence.Configuration;
using Sample.MediatR.Persistence.Context;

namespace Sample.MediatR.Persistence.ServiceRegistration
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IRepository<Order>), typeof(Repository<Order>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(INServiceBus), typeof(NServiceBusService));
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

}
