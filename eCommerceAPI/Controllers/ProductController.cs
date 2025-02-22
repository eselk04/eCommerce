using Domain.Entities;
using eCommerce.Application.Features.Products.Queries.GetAllProducts;
using eCommerce.Application.Interfaces;
using eCommerce.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers;
[Route("api/products")]
public class ProductController : Controller
{
    private readonly IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }
   [HttpGet("getall")]
    public async Task<IActionResult> Index()
    {
        var response = await mediator.Send(new GetAllProductsQueryRequest());
        return Ok(response);
    }
}