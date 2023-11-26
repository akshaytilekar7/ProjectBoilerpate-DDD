using Microsoft.EntityFrameworkCore;

namespace Sample.MediatR.Persistence.Configuration;

public class SqlModelConfiguration : IModelConfiguration
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
    }
}
