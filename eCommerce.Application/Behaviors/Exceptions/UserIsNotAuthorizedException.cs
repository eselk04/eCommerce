using eCommerce.Application.Bases;

namespace eCommerce.Application.Behaviors.Exceptions;

public class UserIsNotAuthorizedException : BaseExceptions
{
    public UserIsNotAuthorizedException() : base("You are not authorized to perform this action.")
    {
        
    }

    public UserIsNotAuthorizedException(string roleName) : base($"You are not {roleName}")
    {
        
    }
}