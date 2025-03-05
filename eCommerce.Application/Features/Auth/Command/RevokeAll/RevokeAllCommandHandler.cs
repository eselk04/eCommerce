using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Interfaces.AutoMapper;
using eCommerce.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Features.Auth.Command.RevokeAll;

public class RevokeAllCommandHandler : BaseHandler ,IRequestHandler<RevokeAllCommandRequest, Unit>
{
    private readonly UserManager<User> _userManager;
    public RevokeAllCommandHandler(IMapper mapper, UserManager<User> userManager , IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
    {
        List<User>? users =  await _userManager.Users.ToListAsync();
        foreach (User user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        return Unit.Value;
    }
}