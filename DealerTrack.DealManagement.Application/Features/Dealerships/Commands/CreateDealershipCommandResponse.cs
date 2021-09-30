using DealerTrack.DealManagement.Application.Responses;

namespace DealerTrack.DealManagement.Application.Features.Dealerships.Commands
{
    public class CreateDealershipCommandResponse: BaseResponse
    {
        public CreateDealershipCommandResponse(): base()
        {

        }

        public CreateDealershipDto Dealership { get; set; }
    }
}