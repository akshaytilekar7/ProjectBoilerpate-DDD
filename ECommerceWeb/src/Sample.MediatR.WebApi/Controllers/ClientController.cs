using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.MediatR.Application.Client.Create;
using Sample.MediatR.Application.Client.Get;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.WebApi.Controllers;

[Route("api/[controller]")]
public class ClientController : Controller
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostClientAsync([FromBody] CreateClientCommand client)
    {
        var id = await _mediator.Send(client);
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
