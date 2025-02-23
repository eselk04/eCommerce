using Domain.Entities;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
{
    private readonly IUnitOfWork unitOfWork;
    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id && !p.isDeleted);
         await unitOfWork.GetWriteRepository<Product>().HardDeleteAsync(product);
         var productCategories = await unitOfWork.GetReadRepository<ProductCategory>()
             .GetAllAsync(p => p.ProductId == request.Id);
         await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);
         await unitOfWork.SaveAsync();
         return new DeleteProductCommandResponse();
    }
}