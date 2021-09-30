using FluentValidation;

namespace DealerTrack.DealManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommandValidator: AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(p => p.VehicleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }
}
