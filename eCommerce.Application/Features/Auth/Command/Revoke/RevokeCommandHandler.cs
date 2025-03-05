using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Auth.Rules;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Features.Auth.Command.Revoke;

public class RevokeCommandHandler : BaseHandler , IRequestHandler<RevokeCommandRequest, Unit>
{
   private readonly UserManager<User> userManager;
   private readonly AuthRules authRules;
    public RevokeCommandHandler(UserManager<User> userManager ,AuthRules authRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.userManager = userManager;
        this.authRules = authRules;
    }

    public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);
        await authRules.EmailAddressShouldBeValid(user);
        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
        return Unit.Value;
    }
}