using FluentValidation;

namespace eCommerce.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(p => p.AccessToken).NotEmpty();
        RuleFor(p => p.RefreshToken).NotEmpty();
    }
}