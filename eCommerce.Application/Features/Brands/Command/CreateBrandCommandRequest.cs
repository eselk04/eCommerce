using MediatR;

namespace eCommerce.Application.Features.Brands.Command;

public class CreateBrandCommandRequest : IRequest<CreateBrandCommandResponse>
{
    public string Name { get; set; }
}