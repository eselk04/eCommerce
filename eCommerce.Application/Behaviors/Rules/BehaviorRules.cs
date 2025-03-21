using eCommerce.Application.Bases;

namespace eCommerce.Application.Behaviors.Rules;

public class BehaviorRules : BaseRules
{
    public  bool UserShouldHaveRequiredRole(IList<string> requiredRoles, IList<string> userRoles)
    {
        bool hasSameElements =  requiredRoles.Intersect(userRoles).Any();
        return hasSameElements;

    }
}