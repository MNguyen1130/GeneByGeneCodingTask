using System;
using System.Collections.Generic;

namespace GeneByGeneCodingTask.Models
{
    public partial class Users
    {
        public Users()
        {
            Samples = new HashSet<Samples>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Samples> Samples { get; set; }
    }
}
