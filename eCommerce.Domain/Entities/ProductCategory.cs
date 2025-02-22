using Domain.Entities.Common;

namespace Domain.Entities;

public class ProductCategory : BaseEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product  Product { get; set; }
    public Category Category { get; set; }
}