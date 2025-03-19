using System.Security.Authentication;
using eCommerce.Application.Behaviors.Exceptions;
using eCommerce.Application.Features.Auth.Command.Login;
using eCommerce.Application.Interfaces.Authorization;
using MediatR;

namespace eCommerce.Application.Behaviors;

public class AuthorizationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IHttpContextAccessor contextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = contextAccessor.HttpContext;
        if(httpContext == null)
            throw new HttpContextIsNullException();
        if(!httpContext.User.Identity.IsAuthenticated && request is not LoginCommandRequest) throw new AuthenticationException("Not Authenticated");
        if (request is IRequireRole roleRequirement && !httpContext.User.IsInRole(roleRequirement.RequiredRole))
            throw new UserIsNotAuthorizedException(roleRequirement.RequiredRole);
        return next();
    }
}   