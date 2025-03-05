using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Auth.Rules;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.Tokens;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler , IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly UserManager<User> userManager;
    private readonly ITokenService tokenService;
    private readonly AuthRules authRules;
    
    public RefreshTokenCommandHandler(IMapper mapper, AuthRules authRules,ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.authRules = authRules;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = principal.FindFirstValue(ClaimTypes.Email);
        User? user = await userManager.FindByEmailAsync(email);
        IList<string> roles =await  userManager.GetRolesAsync(user);
        await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);
        JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
        string newRefreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await userManager.UpdateAsync(user);
        return new RefreshTokenCommandResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        };

    }
}