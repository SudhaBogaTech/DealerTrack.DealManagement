using DealerTrack.DealManagement.Model.Entities;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Application.Features.Deals.Queries;

namespace DealerTrack.DealManagement.Application.Contracts.Persistence
{
    public interface IDealRepository : IAsyncRepository<Deal>    
    {
        Task<List<VehicleStats>> GetMostPopularVehicles();

        Task<List<DealListVm>> GetAllDeals();
        void AddNewDeal(Deal newDeal);
    }
}
