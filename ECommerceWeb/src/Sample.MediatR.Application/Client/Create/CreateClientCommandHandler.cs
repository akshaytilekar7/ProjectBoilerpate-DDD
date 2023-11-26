using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.Application.Client.Create;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Domain.Client>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Domain.Client> repository;

    public CreateClientCommandHandler(IRepository<Domain.Client> context, IMapper mapper, IMediator mediator)
    {
        repository = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Domain.Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Domain.Client>(request);
        var result = await repository.AddAsync(client);

        if (result != null)
            await _mediator.Publish(new ClientCreatedDoaminEvent(result.Id));

        return result;
    }
}
