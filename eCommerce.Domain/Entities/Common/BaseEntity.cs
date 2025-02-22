namespace Domain.Entities.Common;

public class BaseEntity : IBaseEntity
{   
    public DateTimeOffset CreateTime { get; set; }
    public virtual DateTimeOffset UpdatedDate { get; set; }
}