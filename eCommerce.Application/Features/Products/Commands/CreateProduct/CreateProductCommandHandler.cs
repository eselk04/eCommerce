using Domain.Entities;
using eCommerce.Application.Features.Products.Rules;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ProductRules productRules;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,ProductRules productRules)
    {
        this.unitOfWork = unitOfWork;
        this.productRules = productRules;
    }
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();
           await productRules.ProductTitleMustNotBeSame(request.Title , products);
           
        Product product = new(request.Stock, request.Title, request.Description, request.Price, request.Discount, request.Quantity, request.BrandId,request.ImageUrl,request.CategoryIds);
         await unitOfWork.GetWriteRepository<Product>().AddAsync(product);
          var result = await unitOfWork.SaveAsync();
          if (result > 0)
          {
              foreach (var item in request.CategoryIds)
              {
                  await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync
                  (new ProductCategory() {ProductId = product.Id, CategoryId = item});
              }
              await unitOfWork.SaveAsync();
              return new CreateProductCommandResponse($"Success: {result} {(result == 1 ? "item" : "items")} added.");
          }

         
          return new CreateProductCommandResponse("Fail");
    }
}