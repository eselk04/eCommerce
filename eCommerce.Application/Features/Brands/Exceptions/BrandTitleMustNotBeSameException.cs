using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Brands.Exceptions;

public class BrandTitleMustNotBeSameException  : BaseExceptions
{
    public BrandTitleMustNotBeSameException() : base("Brant Title Must Not Be Same")
    {
        
    }
}