using MediatR;

namespace Sample.MediatR.Application.Product.Create;
public class CreateProductCommand : IRequest<string>
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantiity { get; set; }
}
