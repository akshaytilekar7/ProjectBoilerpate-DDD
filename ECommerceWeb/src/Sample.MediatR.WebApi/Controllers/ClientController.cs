using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.MediatR.Application.Client.Create;
using Sample.MediatR.Application.Client.Get;
using Sample.MediatR.Dto;

namespace Sample.MediatR.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : Controller
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostClientAsync([FromBody] CreateClientRequestDto client)
    {
        var cmd = new CreateClientCommand() { CreateClient = client };
        var id = await _mediator.Send(cmd);
        // await _mediator.Publish(new ClientCreatedDoaminEvent(5));
        return Json(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetClientAsync()
    {
        var command = await _mediator.Send(new GetClientsQuery());
        return Json(command);
    }
}
