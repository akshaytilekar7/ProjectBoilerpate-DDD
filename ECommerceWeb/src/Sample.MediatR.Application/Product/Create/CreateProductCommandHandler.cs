using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NServiceBus;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Message;
using Serilog;

namespace Sample.MediatR.Application.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IMessageSession _messageSession;
    private readonly IRepository<Domain.Product> _context;

    public CreateProductCommandHandler(IRepository<Domain.Product> context, IMapper mapper, IMediator _mediator, IMessageSession messageSession)
    {
        _context = context;
        _mapper = mapper;
        this._mediator = _mediator;
        _messageSession = messageSession;
    }
    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Product>(request);

        await _context.AddAsync(entity);
        Log.Information($"add product {entity.Id}");

        await _messageSession.Publish(new ProductCreated() {  Product = entity });

        return await Task.FromResult("Ok");
    }
}
