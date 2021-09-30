using MediatR;

namespace DealerTrack.DealManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommand: IRequest<CreateVehicleCommandResponse>
    {
        public string VehicleName { get; set; }
    }
}
