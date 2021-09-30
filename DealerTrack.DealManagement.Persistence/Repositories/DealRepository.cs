using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Application.Exceptions;
using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Application.Features.Deals.Queries;
using DealerTrack.DealManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Persistence.Repositories
{
    public class DealRepository : BaseRepository<Deal>, IDealRepository
    {
        public DealRepository(DealManagementDbContext dbContext) : base(dbContext)
        {
        }

        public void AddNewDeal(Deal newDeal)
        {
            if (!_dbContext.Deals.Any(m => m.DealNumber == newDeal.DealNumber))
            {
                var vehicle = _dbContext.Vehicles.Where(m => m.VehicleName == newDeal.Vehicle.VehicleName).FirstOrDefault();
                if (vehicle == null)
                {
                    if (!string.IsNullOrEmpty(newDeal.Vehicle.VehicleName))
                        _dbContext.Vehicles.Add(newDeal.Vehicle);
                }
                else
                    _dbContext.Entry(vehicle).State = EntityState.Unchanged;
                var dealership = _dbContext.Dealerships.Where(m => m.DealershipName == newDeal.Dealership.DealershipName).FirstOrDefault();
                if (dealership == null)
                {
                    if (!string.IsNullOrEmpty(newDeal.Dealership.DealershipName))
                        _dbContext.Dealerships.Add(newDeal.Dealership);
                }
                else
                    _dbContext.Entry(dealership).State = EntityState.Unchanged;

                _dbContext.Deals.Add(newDeal);
                _dbContext.SaveChanges();
            }
            else
                throw new NotFoundException("Deal Number", newDeal.DealNumber);
        }

        public async Task<List<DealListVm>> GetAllDeals()
        {
            return  await _dbContext.Deals.Include(m => m.Dealership)
                     .Include(v => v.Vehicle)
                     .Select(m => new DealListVm()
                     {
                         CustomerName = m.CustomerName,
                         DealershipName = m.Dealership.DealershipName,
                         DealNumber = m.DealNumber,
                         Date = m.Date,
                         Price = m.Price,
                         Vehicle = m.Vehicle.VehicleName,
                         
                     })
                     .ToListAsync();
        }

        public async Task<List<VehicleStats>> GetMostPopularVehicles()
        {
            var VehcileList = _dbContext.Deals
                    .GroupBy(p => p.Vehicle.VehicleName)
                    .Select(g => new VehicleStats { Name = g.Key, RepeatCount = g.Count() }).OrderByDescending(a => a.RepeatCount);                     
                   

            if (VehcileList != null && VehcileList.FirstOrDefault() != null)
            {
                return await VehcileList.Where(m => m.RepeatCount == VehcileList.First().RepeatCount).Distinct().ToListAsync();
            }
            return null;
        }

      
    }
}
