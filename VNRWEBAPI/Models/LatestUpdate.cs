using System;
using System.Collections.Generic;

namespace VNRWEBAPI.Models
{
    public partial class LatestUpdate
    {
        public int Id { get; set; }
        public string Information { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int? Createdby { get; set; }

        public virtual User? CreatedbyNavigation { get; set; }
    }
}
