using System;

namespace Sample.MediatR.Application.Client.Get;
public class GetClientsQueryResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
}
