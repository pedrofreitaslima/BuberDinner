using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
  private IMediator _mediator;

  public AuthenticationController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [Route("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = new LoginQuery(request.Email, request.Password);
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(statusCode: StatusCodes.Status401Unauthorized, 
                     title: authResult.FirstError.Description);
    }
    return authResult.Match(
      success => Ok(NewMethod(success)),
      errors => Problem(errors)
      );
  }

  [Route("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = new RegisterCommand(
      request.FirstName, request.LastName,
      request.Email, request.Password);
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

    return authResult.Match(
      success => Ok(NewMethod(success)),
      errors => Problem(errors)
    );
  }

  private static AuthenticationResponse NewMethod(AuthenticationResult authResult)
  {
    return new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
  }
}