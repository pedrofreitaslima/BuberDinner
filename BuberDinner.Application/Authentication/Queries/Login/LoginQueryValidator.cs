using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(e => e.Email).NotEmpty();
        RuleFor(e => e.Password).NotEmpty();
    }
}