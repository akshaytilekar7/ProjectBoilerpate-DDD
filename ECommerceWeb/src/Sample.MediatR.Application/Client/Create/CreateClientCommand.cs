using System;
using MediatR;

namespace Sample.MediatR.Application.Client.Create;
public class CreateClientCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
}
