using ASPWebAPI.DTOs.AdoptionRequest;
using FluentValidation;

namespace ASPWebAPI.Api.Validators.AdoptionRequest
{
    public class UpdateAdoptionRequestDtoValidator : AbstractValidator<UpdateAdoptionRequestDto>
    {
        public UpdateAdoptionRequestDtoValidator()
        {
            RuleFor(x => x.PetId)
            .GreaterThan(0).WithMessage("PetId must be greater than 0.");

            RuleFor(x => x.AdopterId)
                .GreaterThan(0).WithMessage("AdopterId must be greater than 0.");

            RuleFor(x => x.RequestDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("RequestDate cannot be in the future.");

            RuleFor(x => x.AdoptionDate)
                .GreaterThanOrEqualTo(x => x.RequestDate)
                .When(x => x.AdoptionDate != default)
                .WithMessage("AdoptionDate must be after or equal to RequestDate.");

            RuleFor(x => x.Status)
                .Must(status =>
                    new[] { "Pending", "Approved", "Rejected" }
                        .Contains(status, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Status must be one of: Pending, Approved, Rejected.");
        }
    }
}
