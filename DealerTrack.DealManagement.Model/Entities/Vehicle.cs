using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DealerTrack.DealManagement.Model.Entities
{
    public class Vehicle
    {
        public string VehicleName { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int VehicleID { get; set; }
    }
}
