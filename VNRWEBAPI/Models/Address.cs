using System;
using System.Collections.Generic;

namespace VNRWEBAPI.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public string DoorNumber { get; set; } = null!;
        public string? Street { get; set; }
        public string Area { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Pincode { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
