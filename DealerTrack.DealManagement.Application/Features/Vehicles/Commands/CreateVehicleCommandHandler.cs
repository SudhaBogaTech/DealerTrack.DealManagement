using AutoMapper;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Model.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleCommandResponse>
    {
        private readonly IAsyncRepository<Vehicle> _VehicleRepository;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(IMapper mapper, IAsyncRepository<Vehicle> vehicleRepository)
        {
            _mapper = mapper;
            _VehicleRepository = vehicleRepository;
        }

        public async Task<CreateVehicleCommandResponse> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var createVehicleCommandResponse = new CreateVehicleCommandResponse();

            var validator = new CreateVehicleCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createVehicleCommandResponse.Success = false;
                createVehicleCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createVehicleCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createVehicleCommandResponse.Success)
            {
                var vehicle = new Vehicle() { VehicleName = request.VehicleName };
                vehicle = await _VehicleRepository.AddAsync(vehicle);
                createVehicleCommandResponse.Vehicle = _mapper.Map<CreateVehicleDto>(vehicle);
            }

            return createVehicleCommandResponse;
        }
    }
}
