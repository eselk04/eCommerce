using Domain.Entities.Common;
using eCommerce.Application.Features.Brands.Command;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers;
[Route("api/brands")]
public class BrandController : Controller
{
    private readonly IMediator _mediator;
    
    public BrandController(IMediator mediator)
    {
        this._mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommandRequest  request)
    {
       var result = await _mediator.Send(request);
        return Ok(result);
    }
}