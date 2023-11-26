using System.Collections.Generic;
using MediatR;

namespace Sample.MediatR.Application.Order.Get;

public class GetOrderQuery : IRequest<List<Domain.Order>>
{

}
