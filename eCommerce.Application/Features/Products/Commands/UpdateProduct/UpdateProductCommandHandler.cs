using Domain.Entities;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request,
        CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id && !p.isDeleted);
        var map = mapper.Map<Product, UpdateProductCommandRequest>(request);
        var productCategories = await unitOfWork.GetReadRepository<ProductCategory>()
            .GetAllAsync(p => p.ProductId == product.Id);
        await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);
        foreach (var category in request.CategoryIds)
        {
            unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory()
            {
                ProductId = product.Id, CategoryId = category
            });
        }
        await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
        await unitOfWork.SaveAsync();

        return new UpdateProductCommandResponse();
    }
}

