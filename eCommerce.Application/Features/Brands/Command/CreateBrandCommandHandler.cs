using System.Collections;
using Bogus;
using Domain.Entities.Common;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Brands.Rules;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace eCommerce.Application.Features.Brands.Command;

public class CreateBrandCommandHandler : BaseHandler , IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
{
    private readonly BrandRules brandRules;
    public CreateBrandCommandHandler(BrandRules brandRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.brandRules = brandRules;
    }

    public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
    {
       
        IList<Brand> brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();
        brandRules.BrandTitleMustNotBeSame(brands, request.Name);
       var map = mapper.Map<Brand,CreateBrandCommandRequest>(request); 
       await unitOfWork.GetWriteRepository<Brand>().AddAsync(map);
       var result = unitOfWork.SaveAsync();
        return new CreateBrandCommandResponse(){Message = $"Brand {request.Name} created successfully"};
    }
}