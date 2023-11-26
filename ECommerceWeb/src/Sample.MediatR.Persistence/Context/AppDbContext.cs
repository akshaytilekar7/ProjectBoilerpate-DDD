using Microsoft.EntityFrameworkCore;
using Sample.MediatR.Domain;
using Sample.MediatR.Persistence.Configuration;

namespace Sample.MediatR.Persistence.Context;

public class AppDbContext : DbContext
{
    private readonly IModelConfiguration modelConfiguration;

    /// <summary>
    /// Generic DbContextOptions help with IOC configuration especialy it create addition DbContext classes
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions, IModelConfiguration modelConfiguration) : base(dbContextOptions)
    {
        this.modelConfiguration = modelConfiguration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelConfiguration.ConfigureModel(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}




