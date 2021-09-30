using AutoMapper;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Features.Deals.Queries
{
    public class GetMostPopularVehiclesQueryHandler : IRequestHandler<GetMostPopularVehiclesQuery, List<VehicleDTO>>
    {
        private readonly IDealRepository _dealRepository;

        private readonly IMapper _mapper;

        public GetMostPopularVehiclesQueryHandler(IMapper mapper, IDealRepository dealRepository)
        {
            _mapper = mapper;
            _dealRepository = dealRepository;

        }

        public async Task<List<VehicleDTO>> Handle(GetMostPopularVehiclesQuery request, CancellationToken cancellationToken)
        {
            var mostPopularvehicles =   await _dealRepository.GetMostPopularVehicles();
            return _mapper.Map<List<VehicleDTO>>(mostPopularvehicles); 
        }
    }
}
