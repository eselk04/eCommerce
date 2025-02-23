using MediatR;

namespace eCommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandRequest    : IRequest<DeleteProductCommandResponse>
{
    public int Id { get; set; }
    
}