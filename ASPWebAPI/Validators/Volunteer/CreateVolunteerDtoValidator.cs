using ASPWebAPI.DTOs.Volunteer;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.Volunteer
{
    public class CreateVolunteerDtoValidator : AbstractValidator<CreateVolunteerDto>
    {
        public CreateVolunteerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");
        }
    }
}
