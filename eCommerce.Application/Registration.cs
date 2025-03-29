using System.Globalization;
using System.Reflection;
using eCommerce.Application.Bases;
using eCommerce.Application.Behaviors;
using eCommerce.Application.Exceptions;
using eCommerce.Application.Features.Products.Rules;
using FluentValidation;
using MediatR;
using Serilog.Events;
namespace eCommerce.Application;
using Serilog;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddTransient<ExceptionMiddleware>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));
        services.AddValidatorsFromAssembly(assembly);
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) 
            .WriteTo.File(
                path: "/home/eselk/logs/log.json",
                rollingInterval: RollingInterval.Day,
                formatter: new Serilog.Formatting.Json.JsonFormatter())
            .CreateLogger();
        
      
  
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(FluentValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            
    }

    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
        Assembly assembly,Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var t in types)
        {
            services.AddTransient(t);
        }
        return services;
    }
}