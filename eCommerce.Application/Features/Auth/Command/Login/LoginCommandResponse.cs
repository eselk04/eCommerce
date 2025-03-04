namespace eCommerce.Application.Features.Auth.Command.Login;

public class LoginCommandResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
} 