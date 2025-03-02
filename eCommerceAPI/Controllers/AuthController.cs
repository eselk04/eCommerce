using eCommerce.Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers;
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK);
    }
}