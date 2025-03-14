using Domain.Entities.Common;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Brands.Exceptions;

namespace eCommerce.Application.Features.Brands.Rules;

public class BrandRules  : BaseRules
{
    public Task BrandTitleMustNotBeSame(IList<Brand> brands,string brandTitle)
    {
        if (brands.Any(b => b.Name == brandTitle))  
            throw new BrandTitleMustNotBeSameException();
        return Task.CompletedTask;
        
    }
}