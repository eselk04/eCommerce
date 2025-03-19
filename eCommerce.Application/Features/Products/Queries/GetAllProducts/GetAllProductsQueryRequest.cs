using eCommerce.Application.Interfaces.Authorization;
using MediatR;

namespace eCommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryRequest : IRequest<IList<GetAllProductsQueryResponse>> , IRequireRole
{
    public string RequiredRole => "user";
}