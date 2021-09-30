using DealerTrack.DealManagement.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Model.Entities
{
    public class Deal: LastModifiedByEntity
    {
        public int Id { get; set; }
        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public int DealerID { get; set; }

        public int VehicleID { get; set; }

        public Vehicle Vehicle { get; set; }
        public Dealership Dealership { get; set; }

        public Decimal? Price { get; set; }

        public DateTime? Date { get; set; }
    }
}
