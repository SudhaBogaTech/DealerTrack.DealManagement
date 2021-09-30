using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Model.Common
{
    public class LastModifiedByEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
