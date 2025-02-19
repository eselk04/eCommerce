using eCommerce.Application.Interfaces;
using eCommerce.Persistence.Context;
using eCommerce.Infrastructure.Repositories;

namespace eCommerce.Persistence;


public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
    }
}