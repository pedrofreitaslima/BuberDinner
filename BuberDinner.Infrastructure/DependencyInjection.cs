using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    IConfiguration configuration)
  {
    services.AddAuth(configuration)
            .AddPersistence(configuration);
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    return services;
  }

  public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<BuberDinnerDbContext>(options => 
      options.UseSqlServer(configuration.GetConnectionString("BuberDinnerDb")));
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IMenuRepository, MenuRepository>();
    return services;
  }

  public static IServiceCollection AddAuth(this IServiceCollection services, 
    IConfiguration configuration)
  {
    var jwtSettings = new JwtSettings();
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    services.AddAuthentication(
      defaultScheme: JwtBearerDefaults.AuthenticationScheme  
    ).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Secret))
    });
    
    return services;
  }
}