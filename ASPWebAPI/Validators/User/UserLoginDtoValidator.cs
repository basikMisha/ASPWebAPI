using ASPWebAPI.DTOs.User;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.User
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        }
    }
}
