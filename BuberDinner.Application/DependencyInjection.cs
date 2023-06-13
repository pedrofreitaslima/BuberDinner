using System.Reflection;
using BuberDinner.Application.Authentication.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;

namespace BuberDinner.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(typeof(DependencyInjection).Assembly);
    
    services.AddScoped(
      typeof(IPipelineBehavior<,>), 
      typeof(ValidateBehavior<,>));
    
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    return services;
  }
}