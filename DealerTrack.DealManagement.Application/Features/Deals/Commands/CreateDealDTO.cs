using DealerTrack.DealManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Features.Deals.Commands
{
    public class CreateDealDTO
    {
        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public string DealershipName { get; set; }

        public string Vehicle { get; set; }

        public Decimal? Price { get; set; }

        public DateTime? Date { get; set; }

        public Deal Transform()
        {
            return  new Deal()
            {
                CustomerName = this.CustomerName,
                Dealership = new Dealership() { DealershipName = this.DealershipName },
                DealNumber = this.DealNumber,
                Price = this.Price,
                Vehicle = new Vehicle() { VehicleName = this.Vehicle },
                Date = Date
            };
             
        }
    }
}
