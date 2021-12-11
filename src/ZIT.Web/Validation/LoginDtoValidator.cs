using FluentValidation;
using ZIT.Core.DTOs;
using ZIT.Core.Services;

namespace ZIT.Web.Validation;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator(IUserService userService)
    {

        RuleFor(x => x.Email).NotEmpty().MustAsync(async (email, token) =>
        {
            var user = await userService.GetByEmailAsync(email!);

            return user == null;
        });

        RuleFor(x => x.Password).NotEmpty();
    }
}