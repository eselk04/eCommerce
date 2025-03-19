using eCommerce.Application.Bases;

namespace eCommerce.Application.Behaviors.Exceptions;

public class HttpContextIsNullException : BaseExceptions
{
    public HttpContextIsNullException() : base("HttpContext is null")
    {
        
    }
}