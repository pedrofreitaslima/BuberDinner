using BuberDinner.Domain.UserAggregate;

namespace BuberDinner.Application.Authentication.Common;

public record class AuthenticationResult(
  User User,
  string Token
);