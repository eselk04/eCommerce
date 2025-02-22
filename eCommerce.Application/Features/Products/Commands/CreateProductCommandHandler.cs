using Domain.Entities;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
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
              return new CreateProductCommandResponse($"Success: {result} {(result == 1 ? "item" : "items")} added.");
          }

         
          return new CreateProductCommandResponse("Fail");
    }
}