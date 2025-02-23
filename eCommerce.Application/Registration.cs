using System.Globalization;
using System.Reflection;
using eCommerce.Application.Behaviors;
using eCommerce.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace eCommerce.Application;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddTransient<ExceptionMiddleware>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(FluentValidationBehavior<,>));
    }
}