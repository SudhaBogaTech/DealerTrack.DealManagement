using FluentValidation;

namespace DealerTrack.DealManagement.Application.Features.Dealerships.Commands
{
    public class CreateDealershipCommandValidator: AbstractValidator<CreateDealershipCommand>
    {
        public CreateDealershipCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }
}
