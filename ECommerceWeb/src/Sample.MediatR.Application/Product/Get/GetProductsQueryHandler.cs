using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.MediatR.Persistence.Context;

namespace Sample.MediatR.Application.Product.Get;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public GetProductsQueryHandler(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _context.Products.ToListAsync();

        var result = _mapper.Map<List<GetProductsQueryResponse>>(clients);

        return result;
    }
}
