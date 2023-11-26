using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sample.MediatR.Domain.Contracts;

namespace Sample.MediatR.Application.Order.Get;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IEnumerable<Domain.Order>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Order> _repo;

    public GetOrderQueryHandler(IMapper mapper, IRepository<Domain.Order> context)
    {
        _mapper = mapper;
        _repo = context;
    }

    public async Task<IEnumerable<Domain.Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}
