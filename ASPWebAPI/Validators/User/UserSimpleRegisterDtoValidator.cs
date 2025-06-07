using ASPWebAPI.DTOs.User;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.User
{
    public class UserSimpleRegisterDtoValidator : AbstractValidator<UserSimpleRegisterDto>
    {
        public UserSimpleRegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        }
    }
}
