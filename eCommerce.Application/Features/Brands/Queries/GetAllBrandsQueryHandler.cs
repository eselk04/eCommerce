using Domain.Entities.Common;
using eCommerce.Application.Bases;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Brands.Queries;

public class GetAllBrandsQueryHandler : BaseHandler ,IRequestHandler<GetAllBrandsQueryRequest, GetAllBrandsQueryResponse>
{
    public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
    }

    public async Task<GetAllBrandsQueryResponse> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Brand> brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();
        return new GetAllBrandsQueryResponse()
        {   
            Brands = brands
        };
    }
}