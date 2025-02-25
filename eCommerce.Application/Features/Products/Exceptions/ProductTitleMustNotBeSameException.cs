using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Products.Exceptions;

public class ProductTitleMustNotBeSameException : BaseExceptions
{
    public ProductTitleMustNotBeSameException() : base("Product title must not be same") { }
}