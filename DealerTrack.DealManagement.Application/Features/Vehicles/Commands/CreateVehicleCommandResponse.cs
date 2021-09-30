using DealerTrack.DealManagement.Application.Responses;

namespace DealerTrack.DealManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommandResponse: BaseResponse
    {
        public CreateVehicleCommandResponse(): base()
        {

        }

        public CreateVehicleDto Vehicle { get; set; }
    }
}