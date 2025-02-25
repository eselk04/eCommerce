using eCommerce.Application.Bases;
using eCommerce.Application.Features.Products.Exceptions;

namespace eCommerce.Application.Features.Products.Rules;

public class ProductRules : BaseRules
{
    public Task ProductTitleMustNotBeSame(string requesTitle, string productTitle)
    {
        if (requesTitle == productTitle)
        {
            throw new ProductTitleMustNotBeSameException();
        }
        return Task.CompletedTask;
    }
}