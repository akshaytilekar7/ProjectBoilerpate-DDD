using MediatR;
using Sample.MediatR.Dto;

namespace Sample.MediatR.Application.Client.Create;
public class CreateClientCommand : IRequest<CreateClientResponseDto>
{
    public CreateClientRequestDto CreateClient { get; set; }
}
