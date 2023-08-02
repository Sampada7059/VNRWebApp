using System;
using System.Collections.Generic;

namespace VNRWEBAPI.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Gender { get; set; }
        public string Department { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long PhoneNumber { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
