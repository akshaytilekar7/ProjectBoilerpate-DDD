namespace Sample.MediatR.Domain.Contracts;

public interface IRepositoryFactory
{
    IRepository<Client> ClientRepo { get; }
    IRepository<Order> OrderRepo { get; }
    IRepository<Product> ProductRepo { get; }

}
