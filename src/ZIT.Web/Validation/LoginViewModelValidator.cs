using FluentValidation;
using ZIT.Core.DTOs;
using ZIT.Core.Services;
using ZIT.Web.Models;

namespace ZIT.Web.Validation;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator(IUserService userService)
    {
        RuleFor(x => x.Login.Email)
            .NotEmpty()
            .WithName("Email")
            .MustAsync(async (email, token) =>
            {
                var user = await userService.GetByEmailAsync(email!);

                return user != null;
            })
            .WithMessage("Invalid credentials");

        RuleFor(x => x.Login.Password)
            .NotEmpty()
            .WithName("Password");
    }
}