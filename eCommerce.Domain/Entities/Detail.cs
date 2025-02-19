using Domain.Entities.Common;

namespace Domain.Entities;

public class Detail : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
}