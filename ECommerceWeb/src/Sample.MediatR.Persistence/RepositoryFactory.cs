using Sample.MediatR.Domain;
using Sample.MediatR.Domain.Contracts;

namespace Sample.MediatR.Persistence;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IRepository<Client> client;
    private readonly IRepository<Order> order;
    private readonly IRepository<Product> product;

    public RepositoryFactory(IRepository<Client> client, IRepository<Order> order, IRepository<Product> product)
    {
        this.client = client;
        this.order = order;
        this.product = product;
    }

    public IRepository<Client> ClientRepo => client;
    public IRepository<Order> OrderRepo => order;
    public IRepository<Product> ProductRepo => product;

}
