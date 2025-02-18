using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public int Stock { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }    
    public int Quantity { get; set; }     
    
    public string Brand { get; set; }    
    public string ImageUrl { get; set; }  
    public IQueryable<Category> Categories { get; set; }
}