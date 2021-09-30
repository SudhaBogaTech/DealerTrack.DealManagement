using MediatR;

namespace DealerTrack.DealManagement.Application.Features.Dealerships.Commands
{
    public class CreateDealershipCommand: IRequest<CreateDealershipCommandResponse>
    {
        public string Name { get; set; }
    }
}
