namespace Domain.Entities.Common;

public class BaseEntity
{
    public Guid id { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public string Name { get; set; }
    public virtual DateTimeOffset UpdatedDate { get; set; }
}