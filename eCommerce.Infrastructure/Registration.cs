using System.Text;
using eCommerce.Application.Interfaces.Storage;
using eCommerce.Application.Interfaces.Tokens;
using eCommerce.Infrastructure.Enums;
using eCommerce.Infrastructure.Storage;
using eCommerce.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Infrastructure;

public static class Registration
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TokenSettings>(configuration.GetSection("JWT"));
        services.AddTransient<ITokenService, TokenService>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateLifetime = false,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });

        
        
    }
    
    public static void AddStorage<T>(this IServiceCollection services) where T : class,IStorage
    {
        services.AddScoped<IStorage, T>();
    }

    public static void AddStorage(this IServiceCollection services, StorageType.storagetype storageType)
    {
        
        
        switch (storageType)
        {
            case StorageType.storagetype.Local:
                services.AddScoped<IStorage, LocalStorage>();
                break;
            case  StorageType.storagetype.AzureBlob:
                services.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.storagetype.Aws:
           
                break;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}