using System.Security.Authentication;
using System.Security.Claims;
using eCommerce.Application.Behaviors.Exceptions;
using eCommerce.Application.Behaviors.Rules;
using eCommerce.Application.Features.Auth.Command.Login;
using eCommerce.Application.Interfaces.Authorization;
using MediatR;

namespace eCommerce.Application.Behaviors;

public class AuthorizationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> , IRequireRole
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly BehaviorRules behaviorRules;

    public AuthorizationBehavior(IHttpContextAccessor contextAccessor,BehaviorRules rules)
    {
        this.contextAccessor = contextAccessor;
        behaviorRules = rules;
    }
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = contextAccessor.HttpContext;
        if(httpContext == null)
            throw new HttpContextIsNullException();
        if(!httpContext.User.Identity.IsAuthenticated && request is not LoginCommandRequest) throw new AuthenticationException("Not Authenticated");
        List<string> userRoles = httpContext.User.FindAll(ClaimTypes.Role).Select(f => f.Value).ToList();
        if (request is IRequireRole roleRequirement && behaviorRules.UserShouldHaveRequiredRole(request.RequiredRoles , userRoles))
        return next();
        throw new UserIsNotAuthorizedException(string.Join(" or ", request.RequiredRoles));
    }
}   