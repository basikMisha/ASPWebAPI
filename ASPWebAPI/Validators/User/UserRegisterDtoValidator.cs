using ASPWebAPI.DTOs.User;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.User
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required").Must(role =>
                    new[] { "Admin", "User" }
                        .Contains(role, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Role must be one of: Admin, User");
        }
    }
}
