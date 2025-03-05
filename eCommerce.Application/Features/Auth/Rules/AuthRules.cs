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

 public Task EmailOrPasswordShouldNotBeInValid(User? user, bool checkPassword)
 {
  if(user is null || !checkPassword) throw new EmailOrPasswordShouldNotBeInvalidException();
  return Task.CompletedTask;
 }
 public Task RefreshTokenShouldNotBeExpired(DateTime? expirationDate)
 {
  if (expirationDate is null || expirationDate < DateTime.Now) throw new RefreshTokenIsExpiredException();
  return Task.CompletedTask;
 }
}