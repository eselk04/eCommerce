using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public int Stock { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }     
    
    public int BrandId { get; set; }    
    public string ImageUrl { get; set; }
    public Brand Brand { get; set; }
    public ICollection<Category> Categories { get; set; }
}