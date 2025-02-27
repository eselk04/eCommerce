using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string? RefreshToken { get; set; }
    public string? RefreshTokenExpiryTime { get; set; }
}