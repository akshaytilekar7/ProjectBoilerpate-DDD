using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sample.MediatR.Domain.Contracts;

namespace Sample.MediatR.Application.Client.Get;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<GetClientsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Client> _context;

    public GetClientsQueryHandler(IMapper mapper, IRepository<Domain.Client> context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<GetClientsQueryResponse>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _context.GetAllAsync();

        var result = _mapper.Map<List<GetClientsQueryResponse>>(clients);

        return result;
    }
}
