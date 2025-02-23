using System.Text.Json.Serialization;
using MediatR;


namespace eCommerce.Application.Features.Products.Commands;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public int Stock { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }     
    
    public int BrandId { get; set; }    
    public string ImageUrl { get; set; }
    [JsonPropertyName("categories")]
    public List<int> CategoryIds { get; set; } 
}