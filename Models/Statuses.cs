using System;
using System.Collections.Generic;

namespace GeneByGeneCodingTask.Models
{
    public partial class Statuses
    {
        public Statuses()
        {
            Samples = new HashSet<Samples>();
        }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Samples> Samples { get; set; }
    }
}
