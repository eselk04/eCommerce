using eCommerce.Application.Features.Auth.Command.Login;
using eCommerce.Application.Features.Auth.Command.RefreshToken;
using eCommerce.Application.Features.Auth.Command.Register;
using eCommerce.Application.Features.Auth.Command.Revoke;
using eCommerce.Application.Features.Auth.Command.RevokeAll;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK,response);
    }
    [HttpPost("refreshtoken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK,response);
    }
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK);
    }
    [HttpPost("revokeall")]
    public async Task<IActionResult> RevokeAll()
    {
        await _mediator.Send(new RevokeAllCommandRequest());
        return StatusCode(StatusCodes.Status200OK);
    }
}