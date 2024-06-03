using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Features.Identity.Commands.RegisterUser;

namespace NotesApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST api/<IdentityController>
    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] RegisterUserCommand registerUserCommand)
    {
        string token = await _mediator.Send(registerUserCommand);
        return Ok(token);
    }
}
