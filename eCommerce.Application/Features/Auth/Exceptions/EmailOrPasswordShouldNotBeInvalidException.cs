using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Auth.Exceptions;

public class EmailOrPasswordShouldNotBeInvalidException : BaseExceptions
{
    public EmailOrPasswordShouldNotBeInvalidException() : base("Email or password is invalid")
    {
        
    }
}