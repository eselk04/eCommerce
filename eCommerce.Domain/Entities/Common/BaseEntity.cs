namespace Domain.Entities.Common;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public string Name { get; set; }
    public virtual DateTimeOffset UpdatedDate { get; set; }
}