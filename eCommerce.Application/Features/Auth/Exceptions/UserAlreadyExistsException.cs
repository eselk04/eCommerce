using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Auth.Exceptions;

public class UserAlreadyExistsException : BaseExceptions
{
    public UserAlreadyExistsException() : base("User already exists") { }
}