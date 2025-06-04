using ASPWebAPI.DTOs.Adopters;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.Adopter
{
    public class UpdateAdopterDtoValidator : AbstractValidator<UpdateAdopterDto>
    {
        public UpdateAdopterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
        }
    }
}
