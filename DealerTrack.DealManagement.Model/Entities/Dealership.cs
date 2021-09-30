using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DealerTrack.DealManagement.Model.Entities
{
    public class Dealership
    {
        public string DealershipName { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DealerID { get; set; }

    }
}
