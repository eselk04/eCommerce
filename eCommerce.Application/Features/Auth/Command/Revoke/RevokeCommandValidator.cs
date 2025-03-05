using FluentValidation;

namespace eCommerce.Application.Features.Auth.Command.Revoke;

public class RevokeCommandValidator : AbstractValidator<RevokeCommandRequest>
{
    public RevokeCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
    }
}