using Microsoft.EntityFrameworkCore;

namespace Sample.MediatR.Persistence.Configuration;

public interface IModelConfiguration
{
    void ConfigureModel(ModelBuilder modelBuilder);
}
