using System;
using System.Collections.Generic;

namespace VNRWEBAPI.Models
{
    public partial class User
    {
        public User()
        {
            LatestUpdates = new HashSet<LatestUpdate>();
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Status { get; set; }
        public int UserType { get; set; }

        public virtual ICollection<LatestUpdate> LatestUpdates { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
