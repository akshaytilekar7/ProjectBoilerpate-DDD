using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.Application.Client.Create;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Domain.Client> _context;

    public CreateClientCommandHandler(IRepository<Domain.Client> context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        this._mediator = mediator;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Domain.Client>(request);
        var result = await _context.AddAsync(client);
        await _mediator.Publish(new ClientCreatedDoaminEvent(result.Id));
        return await Task.FromResult(result.Id);

    }
}
