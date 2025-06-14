using Domain.Entities.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public ICollection<Detail> Details { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}