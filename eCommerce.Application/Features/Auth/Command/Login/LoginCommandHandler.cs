using System.IdentityModel.Tokens.Jwt;
using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Auth.Rules;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.Tokens;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Features.Auth.Command.Login;

public class LoginCommandHandler : BaseHandler ,  IRequestHandler<LoginCommandRequest,LoginCommandResponse>
{
    private readonly UserManager<User> userManager;
    private readonly AuthRules authRules;
    private readonly RoleManager<Role> roleManager;
    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;
    
    public LoginCommandHandler(UserManager<User> userManager,AuthRules authRules ,IConfiguration configuration,ITokenService tokenService , RoleManager<Role> roleManager,IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.userManager = userManager;
        this.authRules = authRules;
        this.roleManager = roleManager;
        this.tokenService = tokenService;
        this.configuration = configuration;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {   
        User user = await userManager.FindByEmailAsync(request.Email);
        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        await authRules.EmailOrPasswordShouldNotBeInValid(user, checkPassword);
        IList<string> roles = await userManager.GetRolesAsync(user);
        JwtSecurityToken token = await tokenService.CreateToken(user, roles);
        string refreshToken = tokenService.GenerateRefreshToken();
      _ =   int.TryParse(configuration["JWT:RefreshTokenValidityInDays"],out int refreshTokenValidityInDays);
      user.RefreshToken = refreshToken;
      user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);
      await userManager.UpdateAsync(user);
      await userManager.UpdateSecurityStampAsync(user);
      string _token = new JwtSecurityTokenHandler().WriteToken(token);
        await userManager.SetAuthenticationTokenAsync(user,"Default" , "AccessToken", _token);
        return new LoginCommandResponse { Token = _token  , RefreshToken = refreshToken ,Expires = token.ValidTo };
    }
}