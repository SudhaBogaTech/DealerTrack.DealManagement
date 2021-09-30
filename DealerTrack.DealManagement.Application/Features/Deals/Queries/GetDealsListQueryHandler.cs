using AutoMapper;
using DealerTrack.DealManagement.Model.Entities;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Features.Deals.Queries
{
    public class GetDealsListQueryHandler : IRequestHandler<GetDealsListQuery, List<DealListVm>>
    {
        private readonly IDealRepository _dealRepository;        

        private readonly IMapper _mapper;

        public GetDealsListQueryHandler(IMapper mapper, IDealRepository dealRepository)
        {
            _mapper = mapper;
            _dealRepository = dealRepository;
            
            
        }

        public async Task<List<DealListVm>> Handle(GetDealsListQuery request, CancellationToken cancellationToken)
        {
            var allDeals = (await _dealRepository.GetAllDeals()).OrderBy(x => x.DealNumber);
            return _mapper.Map<List<DealListVm>>(allDeals);
        }
    }
}
