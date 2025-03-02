using FluentValidation;

namespace eCommerce.Application.Features.Auth.Command.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty()
            .MaximumLength(50).MinimumLength(2).WithMessage("İsim, soyisim hatası");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().
            WithMessage("Email adresiniz").MinimumLength(8).MaximumLength(80).WithMessage("Email adresiniz");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .MaximumLength(60).WithMessage("Şifre");
        RuleFor(x => x.ConfirmPassword).NotEmpty().
            MinimumLength(8).MaximumLength(60).Equal(x => x.Password).WithMessage("Şifre Eşleşmedi");
    }
}