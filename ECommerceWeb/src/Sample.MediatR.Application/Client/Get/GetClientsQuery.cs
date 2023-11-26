using System.Collections.Generic;
using MediatR;

namespace Sample.MediatR.Application.Client.Get;

public class GetClientsQuery : IRequest<List<GetClientsQueryResponse>>
{

}
