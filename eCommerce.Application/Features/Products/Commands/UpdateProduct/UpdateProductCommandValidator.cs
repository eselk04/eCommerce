using FluentValidation;

namespace eCommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithName("Id");
        RuleFor(x=>x.Title).NotEmpty().WithName("Başlık");
        RuleFor(x => x.Description).NotEmpty().WithName("Açıklama");
        RuleFor(x=>x.BrandId).NotEmpty().WithName("Marka");
        RuleFor(x=>x.Price).NotEmpty().WithName("Fiyat");
        RuleFor(x=>x.Quantity).NotEmpty().WithName("Miktar");
        RuleFor(x=>x.Discount).NotEmpty().WithName("İndirim Oranı");
        RuleFor(x=>x.CategoryIds).NotEmpty().Must(categories=>categories.Any()).WithName("Kategoriler");
    }
}