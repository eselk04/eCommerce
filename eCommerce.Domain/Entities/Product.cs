using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : IBaseEntity
{
    public Product(int stock, string title, string description, decimal price, decimal discount, int quantity,
        int brandId, string imageUrl, IList<int> requestCategories)
    {
        Stock = stock;
        Title = title;
        Description = description;
        Price = price;
        Discount = discount;
        Quantity = quantity;
        BrandId = brandId;
        ImageUrl = imageUrl;
    }

    public Product()
    {
        
    }

    public int Id { get; set; }
    public int Stock { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }     
    
    public int BrandId { get; set; }    
    public string ImageUrl { get; set; }
    public Brand Brand { get; set; }
    
    public bool isDeleted { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
   
}