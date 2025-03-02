using Domain.Entities;
using eCommerce.Application.Bases;
using eCommerce.Application.Features.Auth.Exceptions;

namespace eCommerce.Application.Features.Auth.Rules;

public class AuthRules : BaseRules
{
 public Task UserShouldNotBeExist(User? user)
 {
  if(user is null) return Task.CompletedTask;
  throw new UserAlreadyExistsException();
 }
}