using ASPWebAPI.DTOs.Adopters;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.Adopter
{
    public class CreateAdopterDtoValidator : AbstractValidator<CreateAdopterDto>
    {
        public CreateAdopterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        }
    }
}
