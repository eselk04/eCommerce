using System.Reflection.PortableExecutable;
using FluentValidation;

namespace eCommerce.Application.Features.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidator()
    {
     RuleFor(x=>x.Title).NotEmpty().WithName("Başlık");
     RuleFor(x => x.Description).NotEmpty().WithName("Açıklama");
     RuleFor(x=>x.BrandId).NotEmpty().WithName("Marka");
     RuleFor(x=>x.Price).NotEmpty().WithName("Fiyat");
     RuleFor(x=>x.Quantity).NotEmpty().WithName("Miktar");
     RuleFor(x=>x.Discount).NotEmpty().WithName("İndirim Oranı");
     RuleFor(x=>x.CategoryIds).NotEmpty().Must(categories=>categories.Any()).WithName("Kategoriler");
     
     
    }
}