namespace eCommerce.Application.Interfaces.Authorization;

public interface IRequireRole
{
    public IList<string> RequiredRoles { get; }          
}