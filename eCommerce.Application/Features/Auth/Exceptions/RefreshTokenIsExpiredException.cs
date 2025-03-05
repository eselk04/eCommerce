using eCommerce.Application.Bases;

namespace eCommerce.Application.Features.Auth.Exceptions;

public class RefreshTokenIsExpiredException : BaseExceptions
{
    public RefreshTokenIsExpiredException() : base("Oturum süresi sona erdi. Tekrar giriş yapınız.") { }
}