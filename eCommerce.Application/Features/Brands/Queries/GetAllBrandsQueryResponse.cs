using Domain.Entities.Common;

namespace eCommerce.Application.Features.Brands.Queries;

public class GetAllBrandsQueryResponse
{
    public string Message => $"{Brands.Count} brands found.";

    public IList<Brand> Brands { get; set; }
}