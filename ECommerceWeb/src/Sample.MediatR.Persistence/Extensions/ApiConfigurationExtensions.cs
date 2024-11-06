using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Sample.MediatR.WebApi.Core.Middlewares;
namespace Sample.MediatR.Persistence.Extensions;
public static class ApiConfigurationExtensions
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {   
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();
        app.MapControllers();
        app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        //app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
