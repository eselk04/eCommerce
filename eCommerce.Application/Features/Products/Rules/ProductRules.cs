using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Products.Exceptions;

namespace eCommerce.Application.Features.Products.Rules;

public class ProductRules : BaseRules
{
    public Task ProductTitleMustNotBeSame(string requesTitle, IList<Product> products)
    {
        if (products.Any(p => p.Title == requesTitle)) throw new ProductTitleMustNotBeSameException();
        return Task.CompletedTask;
    }
}