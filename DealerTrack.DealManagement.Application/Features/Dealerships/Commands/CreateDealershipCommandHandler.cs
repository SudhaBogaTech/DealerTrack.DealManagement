using AutoMapper;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Model.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Features.Dealerships.Commands
{
    public class CreateDealershipCommandHandler : IRequestHandler<CreateDealershipCommand, CreateDealershipCommandResponse>
    {
        private readonly IAsyncRepository<Dealership> _dealershipRepository;
        private readonly IMapper _mapper;

        public CreateDealershipCommandHandler(IMapper mapper, IAsyncRepository<Dealership> dealershipRepository)
        {
            _mapper = mapper;
            _dealershipRepository = dealershipRepository;
        }

        public async Task<CreateDealershipCommandResponse> Handle(CreateDealershipCommand request, CancellationToken cancellationToken)
        {
            var createDealershipCommandResponse = new CreateDealershipCommandResponse();

            var validator = new CreateDealershipCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createDealershipCommandResponse.Success = false;
                createDealershipCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createDealershipCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createDealershipCommandResponse.Success)
            {
                var dealership = new Dealership() { DealershipName = request.Name };
                dealership = await _dealershipRepository.AddAsync(dealership);
                createDealershipCommandResponse.Dealership = _mapper.Map<CreateDealershipDto>(dealership);
            }

            return createDealershipCommandResponse;
        }
    }
}
