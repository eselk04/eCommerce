using Domain.Entities;
using Domain.Entities.Common;
using eCommerce.Application.DTOs;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand));

        var brand = mapper.Map<BrandDto, Brand>(new Brand());

        var map = mapper.Map<GetAllProductsQueryResponse, Product>(products);
        foreach (var item in map)
            item.Price -= (item.Price * item.Discount / 100);

        return map;
    }
}