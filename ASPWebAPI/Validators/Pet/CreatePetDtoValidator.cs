using ASPWebAPI.DTOs.Pet;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.Pet
{
    public class CreatePetDtoValidator : AbstractValidator<CreatePetDto>
    {
        public CreatePetDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 100).WithMessage("Name should be min 2 symbols and max 100");

            RuleFor(x => x.Species)
                .NotEmpty().WithMessage("Species is required")
                .Length(2, 50).WithMessage("Species should be min 2 symbols and max 100");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Name is required")
                .GreaterThanOrEqualTo(0).WithMessage("Age can't be negative");

            RuleFor(x => x.IsAdopted)
            .NotNull().WithMessage("Adoption status is required (true/false)");
        }
    }
}
