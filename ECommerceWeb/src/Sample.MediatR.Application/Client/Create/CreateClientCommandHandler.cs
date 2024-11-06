using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sample.MediatR.Domain.Contracts;
using Sample.MediatR.Dto;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.Application.Client.Create;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepositoryFactory repositoryFactory;

    public CreateClientCommandHandler(IRepositoryFactory repositoryFactory, IMapper mapper, IMediator mediator)
    {
        this.repositoryFactory = repositoryFactory;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateClientResponseDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Domain.Client>(request.CreateClient);
        var result = await repositoryFactory.ClientRepo.AddAsync(client);
        var response = _mapper.Map<CreateClientResponseDto>(result);

        if (result != null)
            await _mediator.Publish(new ClientCreatedDoaminEvent(result.Id));

        return response;
    }
}
