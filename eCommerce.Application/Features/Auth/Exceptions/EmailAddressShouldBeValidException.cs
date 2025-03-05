using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Auth.Exceptions;

public class EmailAddressShouldBeValidException : BaseExceptions
{
    public EmailAddressShouldBeValidException() : base("Email address is invalid")
    {
        
    }
}