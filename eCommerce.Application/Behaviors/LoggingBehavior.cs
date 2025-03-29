using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Domain.Entities;
using Domain.Entities.Logging;
using eCommerce.Application.Interfaces.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> , IRequireRole
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;
    private readonly IMediator mediator;
    private HttpContext? httpContext;


    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        UserManager<User> userManager, RoleManager<Role> roleManager, IMediator mediator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.mediator = mediator;
        httpContext = httpContextAccessor.HttpContext;

    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {   string requestName = typeof(TRequest).Name;
        string requestId = Guid.NewGuid().ToString();
        
        var (userId, userName, ipAddress) = await GetUserAndIpInfo();
        logger.LogInformation($"[START] {requestName} ({requestId}) by {userName} ({userId}) from {ipAddress}");
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

      
            var response = await next();
            
            stopwatch.Stop();
            logger.LogInformation($"[END] {requestName} ({requestId}); Execution time={stopwatch.ElapsedMilliseconds}ms");
            logger.LogDebug($"Response data: {response}");
            
            if (httpContext is not null && HasAnyRole("Any","Role","ForDbLogging"))
            {
                await mediator.Publish(new LogEntryCreated(
                    requestId,
                    requestName,
                    DateTime.UtcNow,
                    stopwatch.ElapsedMilliseconds,
                    "Success",
                    string.Empty,
                    string.Empty,
                    JsonSerializer.Serialize(request),
                    JsonSerializer.Serialize(response),
                    userId,
                    userName,
                    ipAddress), cancellationToken);
            }
         

        
       
        return response;
    }
    
    
    private async Task<(string userId, string userName, string ipAddress)> GetUserAndIpInfo()
    {
        if (httpContext == null)
        {
            return (string.Empty, string.Empty, string.Empty);
        }

       
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        
        var user = httpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            return (string.Empty, string.Empty, ipAddress);
        }
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = user.Identity.Name;
        
        return (userId, userName, ipAddress);
    }
    public bool HasAnyRole(params string[] roles)
    {
        return roles.Any(role => httpContext.User.IsInRole(role));
    }
}