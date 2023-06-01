using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
  private readonly IMediator _mediator;
  private readonly IMapper _mapper;

  public AuthenticationController(IMediator mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [Route("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = _mapper.Map<LoginQuery>(request);
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(statusCode: StatusCodes.Status401Unauthorized, 
                     title: authResult.FirstError.Description);
    }
    return authResult.Match(
      success => Ok(_mapper.Map<AuthenticationResponse>(success)),
      errors => Problem(errors)
      );
  }

  [Route("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = _mapper.Map<RegisterCommand>(request);
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

    return authResult.Match(
      success => Ok(_mapper.Map<AuthenticationResponse>(success)),
      errors => Problem(errors)
    );
  }
}