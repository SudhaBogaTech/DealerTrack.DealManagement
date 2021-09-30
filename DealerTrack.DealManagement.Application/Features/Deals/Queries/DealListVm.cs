using DealerTrack.DealManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Features.Deals.Queries
{
    public class DealListVm
    {
        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public string DealershipName { get; set; }

        public string Vehicle { get; set; }

        public Decimal? Price { get; set; }

        public DateTime? Date { get; set; }

    }
}
