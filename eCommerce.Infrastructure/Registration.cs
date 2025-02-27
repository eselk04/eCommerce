using eCommerce.Infrastructure.Tokens;

namespace eCommerce.Infrastructure;

public static class Registration
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TokenSettings>(configuration.GetSection("JWT"));
    }
}