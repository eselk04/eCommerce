using Domain.Entities;
using eCommerce.Application.Interfaces;
using eCommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers;
[Route("products")]
public class ProductController : Controller
{
    private IReadRepository<Product> _productRepository;

    public ProductController(IReadRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet("getall")]
    public IActionResult Index()
    {
        return  Ok(_productRepository.GetAllAsync());
    }
}