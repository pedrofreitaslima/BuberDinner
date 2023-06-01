using BuberDinner.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    IConfiguration configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IUserRepository, UserRepository>();
    return services;
  }
}